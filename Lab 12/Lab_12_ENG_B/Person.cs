using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Lab_12_ENG_B
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        [XmlIgnore]
        public List<Prize> Prizes { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }

    [Serializable]
    public class Prize
    {
        public int Place { get; set; }
        public Contest Contest { get; set; }

        public override string ToString()
        {
            return $"{Place} at {Contest}";
        }
    }

    [Serializable]
    public class Contest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public List<Person> Participants { get; set; }

        public override string ToString()
        {
            return $"{Name} took place on {Date}";
        }
    }

    public class Scoreboard : IEnumerable
    {
        public List<Person> participants;
        public List<Contest> contests;
        public string currentPath { get; set; }

        public Scoreboard(string name = "")
        {
            if (!name.Equals(""))
            {
                currentPath = name;
                if (string.IsNullOrEmpty(currentPath))
                {
                    throw new Exception("current path is empty");
                }
                else
                {
                    try
                    {
                        if (Directory.Exists(currentPath))
                        {
                            throw new PathAlreadyExistsException();
                        }

                        var di = Directory.CreateDirectory(name);
                        participants = new List<Person>();
                        contests = new List<Contest>();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            else
            {
                participants = new List<Person>();
                contests = new List<Contest>();
            }
        }

        public void Add(Contest contest)
        {
            string filename = Path.Combine(currentPath, $"{contest.Name}.json");
            if (File.Exists(filename))
            {
                throw new PathAlreadyExistsException();
            }
            var json = JsonSerializer.Serialize(contest, new JsonSerializerOptions {
                WriteIndented = true
            });
            contests.Add(contest);
            foreach (Person p in contest.Participants)
                participants.Add(p);

            File.WriteAllText(filename, json);
        }

        public void Info(bool printParticipants = true)
        {
            if (printParticipants)
            {
                foreach (Person p in participants)
                {
                    Console.WriteLine(p);
                    foreach (Prize pr in p.Prizes)
                        Console.WriteLine("     " + pr);
                }
            }
            foreach (Contest c in contests)
                Console.WriteLine(c);
            //foreach(Contest c in contests)
            //{
            //    Console.WriteLine(c);
            //    if (printParticipants == true)
            //    {
            //        foreach (Person p in c.Participants)
            //        {
            //            Console.WriteLine(p);
            //            Console.WriteLine(p.Prizes);
            //        }
            //    }
            //}
        }

        public IEnumerator<Contest> GetEnumerator()
        {
            string[] str = Directory.GetFiles(currentPath);
            foreach(string s in str)
            {
                string json = File.ReadAllText(s);
                yield return JsonSerializer.Deserialize<Contest>(json);
            }

        }

        public void Update(Contest c)
        {
            string filename = Path.Combine(currentPath, $"{c.Name}.json");
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }
            File.Delete(filename);
            Add(c);
        }

        public void Delete(string c)
        {
            string filename = Path.Combine(currentPath, $"{c}.json");
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }
            File.Delete(filename);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Scoreboard Create(string source)
        {
            Scoreboard score = new Scoreboard();
            BinaryFormatter bf = new BinaryFormatter();
            //string filename = Path.Combine(score.currentPath, source);
            FileStream fs = new FileStream(source, FileMode.Open);
            List<Contest> contest = (List<Contest>)bf.Deserialize(fs);
            foreach (Contest c in contest)
            {
                score.contests.Add(c);
                foreach (Person p in c.Participants)
                {
                    if (!score.participants.Contains(p))
                        score.participants.Add(p);
                }
            }
            return score;
        }

        public void Save(string filenamepath)
        {
            try
            {
                using (var fs = new FileStream(filenamepath, FileMode.Open))
                {
                    var parent_xml_serializer = new XmlSerializer(typeof(List<Person>));
                    parent_xml_serializer.Serialize(fs, participants);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization failed - {ex?.Message}");
            }
        }
    }

    public class PathAlreadyExistsException : Exception
    {
        public int errorCode;
        public PathAlreadyExistsException(string mess, Exception inner) : base(mess, inner) { }
        public PathAlreadyExistsException(string mess) : base(mess) { }
        public PathAlreadyExistsException() { }
        public PathAlreadyExistsException(int ec) { errorCode = ec; }
    }
}
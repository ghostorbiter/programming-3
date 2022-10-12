using System;
using System.Text;

namespace Lab5BEn
{
    public class PostOffice
    {
        private int Size;
        private PostBox[] postboxes;
        private int Count;
        public PostOffice(int size)
        {
            Size = size;
            postboxes = new PostBox[Size];
        }

        public int GetPostBoxesCount()
        {
            return Count;
        }

        public bool AddPostBox(PostBox postbox)
        {
            if (Count >= Size)
                return false;
            postboxes[Count++] = postbox;
            return true;
        }

        public void Print()
        {
            Console.WriteLine($"Post office with {Count} post boxes");
            Console.WriteLine("Post boxes: ");
            for (int i = 0; i < Count; ++i)
            {
                Console.WriteLine($"{i}");
                postboxes[i].Print();
            }
        }
    }
}
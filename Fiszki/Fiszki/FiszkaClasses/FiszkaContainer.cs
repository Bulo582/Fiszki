using System;
using System.Collections.Generic;
using System.Text;
using Fiszki.Classes;

namespace Fiszki.FiszkaClasses
{
    class FiszkaContainer
    {
        public Dictionary<string, string> D_Fiszki = new Dictionary<string, string>();
        public string[] pl;
        public string[] ang;
        public int correct = 0;
        public List<string> incorrectWords = new List<string>();

        private string title;
        public string Title
        {
            get { return this.title; }
        }
        private string[] context;

        public FiszkaContainer()
        {

        }

        public FiszkaContainer(string fiszka)
        {
            Separate(fiszka);
            Implement();
        }

        public void Add(string fiszka)
        {
            Separate(fiszka);
            Implement();
        }

        /// <summary>
        /// implemet fiszka title and fiszka context
        /// </summary>
        /// <param name="fiszka"></param>
        private void Separate(string fiszka)
        {
            string text = fiszka;
            title = text.Substring(0, text.IndexOf('$'));

            text = text.Remove(0, text.IndexOf('$') + 1);
            context = text.Split(new string[] { "\n" }, options: StringSplitOptions.RemoveEmptyEntries);
            context = text.Split(new string[] {"#", "\r",".","\\","\n"}, options: StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// !Separate first! Implement dictionary - key is anglish, value is polish translation
        /// </summary>
        private void Implement()
        {
            pl = new string[context.Length/2];
            ang = new string[context.Length/2];
            int index = 0;
            for (int i = 0; i < context.Length; i = i + 2)
            {
                D_Fiszki.Add(context[i], context[i + 1]);

                ang[index] = context[i];
                pl[index] = context[i + 1];
                index++;

                if (context.Length <= i + 1)
                    break;
            }
        }

        public string InCorrectFiszkaFormat
        {
            get
            {
                string format = String.Empty;
                foreach (string item in incorrectWords)
                {
                    D_Fiszki.TryGetValue(item, out string pl);
                    format += (item + "#" + pl + ".");
                }
                return format;
            }
        }
    }
}

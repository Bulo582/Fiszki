using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fiszki.Classes
{
    class EmbeddFiles
    {

        private StreamReader reader;
        private string text;

        /// <summary>
        /// Bug flag 
        /// </summary>
        public bool Alert {get; set;}

       public EmbeddFiles(string filePath)
        {
            Alert = false;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(filePath);
            reader = new StreamReader(stream);
            CompleteText();
            reader.Close();
            stream.Close();
        }

        private void CompleteText()
        {
            if (reader.Peek() > 0)
            {
                text = reader.ReadToEnd();
            }
            else
                Alert = true;
        }

        public string Text
        {
            get { return this.text; }
        }

    }
}

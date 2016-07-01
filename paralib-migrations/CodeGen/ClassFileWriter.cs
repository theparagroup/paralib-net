using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Migrations.CodeGen
{
    public class ClassFileWriter : IClassWriter
    {
        protected string _path;
        protected bool _replace;
        protected bool _exists;
        protected StreamWriter _streamWriter;

        public ClassFileWriter(FileOptions fileOptions)
        {
            string slash = new string(Path.DirectorySeparatorChar, 1);

            _path = fileOptions.Path??".\\";

            if (_path.Trim()!="" && !_path.EndsWith(slash))
            {
                _path += slash;
            }

            if (fileOptions.SubDirectory!=null)
            {
                _path += fileOptions.SubDirectory + slash;
            }

            _replace = fileOptions.Replace;
            _streamWriter = null;
        }

        public void Start(string className)
        {
            if (_streamWriter != null)
            {
                throw new ParalibException("FileWriter is already started");
            }

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            string filePath = $"{_path}{className}.cs";

            _exists = File.Exists(filePath);

            if (_replace || !_exists)
            {
                _streamWriter = new StreamWriter(filePath);
            }

        }

        public void Write(string text)
        {
            if (_replace || !_exists)
            {
                if (_streamWriter != null)
                {
                    _streamWriter.Write(text);
                }
            }
        }

        public void WriteLine(string text=null)
        {
            Write($"{text}\n");
        }

        public void End()
        {
            if (_replace || !_exists)
            {
                if (_streamWriter != null)
                {
                    _streamWriter.Close();
                    _streamWriter = null;
                }
            }
        }

    }
}

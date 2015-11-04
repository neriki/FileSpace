/* 
 File Space
 The MIT License(MIT) 
  
 Copyright(c) 2015 Eric Boniface 
  
  Permission is hereby granted, free of charge, to any person obtaining a copy 
  of this software and associated documentation files (the "Software"), to deal 
  in the Software without restriction, including without limitation the rights 
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
  copies of the Software, and to permit persons to whom the Software is 
  furnished to do so, subject to the following conditions: 
  
 The above copyright notice and this permission notice shall be included in 
  all copies or substantial portions of the Software. 
  
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
  THE SOFTWARE. 
  
 */

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace FileSpace
{
    public class ClassFile:ClassFsItem
    {
        private string _md5;

        public List<ClassFile> lstDuplicate { private get; set; }

        public override int nbDuplicate
        {
            get
            {
                return lstDuplicate.Count-1;
            }
        }

        public ClassFile(double size, string name, ClassDirectory parent)
        {
            Type = "File";
            Name = name;
            Parent = parent;
            Size = size;
            deleteText = "Do you want to delete the file: \r\n " + Name;
        }

        public ClassFile(double size, string name, ClassDirectory parent, string md5) : this(size,name,parent)
        {
            _md5 = md5;
        }


        public override void OpenInExplorer()
        {
            Process.Start("Explorer.exe", Parent.Name);
        }

        public string Md5()
        {
            if (_md5 == null) { 
                using (var md5 = MD5.Create())
                {
                
                    using (var stream = File.OpenRead(Name))
                    {
                        byte[] md5Raw= md5.ComputeHash(stream);
                        _md5 = System.BitConverter.ToString(md5Raw);
                        stream.Close();
                    }
                }
            }
            return _md5;
        }

        public override void deleteFile()
        {
            Parent.Remove(this);
            duplicateFile.deleteFile(this);
        }
    }
}

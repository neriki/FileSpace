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


using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileSpace
{
    public class ClassDirectory : ClassFsItem, IEnumerable<ClassFsItem>
    {

        private List<ClassFsItem> _content;

        public override int nbDuplicate
        {
            get
            {
                return 0;
            }
        }

        public ClassDirectory(string name) :this(name,null)
        {
            
            duplicateFile = new ClassDuplicate();
        }


        private ClassDirectory(string name, ClassDirectory parent)
        {
            Type = "Dir.";
            Size = 0;
            Name = name;
            Parent = parent;
            _content=new List<ClassFsItem>();
            deleteText = "Do you want to delete the directory: \r\n " +
                            Name + "\r\n and all files and directories in this directory.";
        }

        public void Add(ClassFsItem newItem)
        {
            _content.Add(newItem);
        }

        public void Remove(ClassFsItem rmItem)
        {
            _content.Remove(rmItem);
        }

        public ClassDirectory AddDirectory(string name)
        {
            ClassDirectory newItem =new ClassDirectory(name,this);
            _content.Add(newItem);
            return newItem;
        }

        public ClassFile AddFile(string name, double size)
        {
            ClassFile newItem =new ClassFile(size,name, this);
            _content.Add(newItem);
            duplicateFile.add(newItem);
            return newItem;
        }

        public ClassFile AddFile(string name, double size, string md5)
        {
            ClassFile newItem = new ClassFile(size, name, this, md5);
            _content.Add(newItem);
            duplicateFile.add(newItem);
            return newItem;
        }


        public IEnumerator<ClassFsItem> GetEnumerator()
        {
            return ((IEnumerable<ClassFsItem>) _content).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void OpenInExplorer()
        {
            Process.Start("Explorer.exe", Name);
        }

        public override void deleteFile()
        {
            Parent.Remove(this);
            duplicateFile.deleteFile(this);
        }

        public bool empty()
        {
            return _content.Count == 0;
        }
    }
}

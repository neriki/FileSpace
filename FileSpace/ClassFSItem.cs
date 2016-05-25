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
using System;

namespace FileSpace
{
    public abstract class ClassFsItem : IEquatable<ClassFsItem>
    {
        private double _size;

        public string Name { get; set; }

        public abstract int NbDuplicate { get; }

        public string DeleteText { get; protected set; }

        private ClassDuplicate _duplicateFile;

        public ClassDuplicate DuplicateFile {
            get {
                if (Parent != null)
                    return Parent.DuplicateFile;
                return _duplicateFile;
            }
            protected set{
                if (Parent != null)
                    Parent.DuplicateFile = value;
                _duplicateFile = value;
            }
        }


        public double Size
        {
            get { return _size; }
            set
            {
                _size = value;
                if (Type == "File" && Parent != null)
                    Parent.AddSize(value);
            }
        }

        public double AddSize(double value)
        {
            _size += value;
            if (Parent != null)
                Parent.AddSize(value);
            return _size;
        }

        public ClassDirectory Parent { get; protected set; }
        public string Type { get; protected set; }

        public double RelativeSize
        {
            get { return Size/MaxSize; }
        }

        protected double MaxSize
        {
            get
            {
                if (Parent != null)
                    return Parent.MaxSize;
                
                return Size;
            }
        }

        public abstract void OpenInExplorer();

        public abstract void DeleteFile();

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(ClassFsItem other)
        {
            if (other == null) return false;
            return Name.Equals(other.Name);
        }
    }
    
}

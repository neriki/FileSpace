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
using System.IO;

namespace FileSpace
{
    public class ClassDuplicate : IEnumerable<ClassFile>
    {
        //List of duplicate file
        private Dictionary<string, List<ClassFile>> _duplicateFile;

        public string searchedMd5 { get; set; }

        public ClassDuplicate()
        {
            _duplicateFile = new Dictionary<string, List<ClassFile>>();
        }

        public void add(ClassFile file)
        {
            string fileMd5 = file.Md5();
            if (_duplicateFile.ContainsKey(fileMd5))
            {
                if (!_duplicateFile[fileMd5].Contains(file))
                    _duplicateFile[fileMd5].Add(file);
            }
            else
            {
                _duplicateFile.Add(fileMd5, new List<ClassFile>());
                _duplicateFile[fileMd5].Add(file);
            }

            file.lstDuplicate = _duplicateFile[fileMd5];
        }

        public void deleteFile(ClassFsItem itemToDelete)
        {
            if (itemToDelete.Type == "File") { 
                _duplicateFile[((ClassFile)itemToDelete).Md5()].Remove(((ClassFile)itemToDelete));
                File.Delete(itemToDelete.Name);
            }
            else
            {
                deleteRepDuplicate(((ClassDirectory)itemToDelete));
                Directory.Delete(itemToDelete.Name, false);
            }
            
        }

        private void deleteRepDuplicate(ClassDirectory rep)
        {
            foreach (ClassFsItem item in rep)
            {
                if (item.Type == "File") { 
                    _duplicateFile[((ClassFile)item).Md5()].Remove(((ClassFile)item));
                    File.Delete(item.Name);
                }
                else { 
                    deleteRepDuplicate(((ClassDirectory)item));
                    Directory.Delete(item.Name, false);
                }
            }
        }


        public IEnumerator<ClassFile> GetEnumerator()
        {
            //if (!_duplicateFile.ContainsKey(searchedMd5))  //TODO: throw exception
            return ((IEnumerable<ClassFile>)_duplicateFile[searchedMd5]).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSpace
{
    class ClassScan
    {
        //List of duplicate file
        //private Dictionary<string, List<ClassFile>> _duplicateFile; 

        //Error list
        private List<string> _lstErr;

        //root element of the file system scanned
        private ClassDirectory _root;

        public string ErrorList
        {
            get
            {
                if(_lstErr.Count>0) 
                    return _lstErr.Aggregate("List of errors:\r\n\r\n", (current, err) => current + err + "\r\n-------------------------------------------------------------");
                return "";
            }
        }

        public ClassScan()
        {
            _lstErr=new List<string>();
        }

        public void Scan(string path, MainWindow owindow)
        {
            _root=new ClassDirectory(path);
            _lstErr.Clear();

            //search
            ScanRep(_root);

            //show
            Show(owindow);

        }

        public void Show(MainWindow owindow)
        {
            //show
            owindow.ClearList();

            owindow.AddLine(_root);
            ShowRep(_root, owindow);
        }

        public void ShowDuplicate( string md5Sum,MainWindow owindow)
        {
            //affichage
            owindow.ClearList();

            _root.DuplicateFile.SearchedMd5 = md5Sum;

            foreach (ClassFile oFile in _root.DuplicateFile)
                owindow.AddLine(oFile);
        }

        private void ScanRep(ClassDirectory root)
        {
            //Listing files
            try
            {
                var lFiles = Directory.EnumerateFiles(root.Name);
                foreach (string f in lFiles) { 
                    root.AddFile(f, new FileInfo(f).Length);   
                }
            }
            catch (Exception e)
            {
                _lstErr.Add(root.Name + "\r\n" + e.Message + "\r\n");
            }
            //Listing directories
            try
            {
                var lDir = Directory.EnumerateDirectories(root.Name);
                foreach (string d in lDir)
                {
                    ScanRep(root.AddDirectory(d)); //And we step through
                }
            }
            catch (Exception e)
            {
                _lstErr.Add(root.Name + "\r\n" + e.Message + "\r\n");
            }
        }

        private void ShowRep(ClassDirectory root, MainWindow owindow)
        {
            foreach (ClassFsItem itm in root)
            {
                owindow.AddLine(itm);
                if (itm.Type == "Dir.")
                {
                    ShowRep((ClassDirectory) itm, owindow);
                }
            }
        }

        public void DeleteFile(ClassFsItem itemToDelete, MainWindow owindow)
        {
            itemToDelete.DeleteFile();
            owindow.ListFile.Items.Remove(itemToDelete);
            
            owindow.ListFile.Items.Refresh();
        }
    }
}

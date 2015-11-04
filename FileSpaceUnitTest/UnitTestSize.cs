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

using FileSpace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSpaceUnitTest
{
    [TestClass]
    public class UnitTestSize
    {
        [TestMethod]
        public void TestMethodSizeOneFileTwoDir()
        {
            ClassDirectory root=new ClassDirectory("c:\\test");

            ClassDirectory test2 = root.AddDirectory("c:\\test\\test2");

            ClassDirectory test3 = test2.AddDirectory("c:\\test\\test2\\test3");

            ClassFile file1 = test3.AddFile("c:\\test\\test2\\test3\\file1", 80, "123");

            Assert.AreEqual(root.Size,file1.Size);

        }


        [TestMethod]
        public void TestMethodSizeTwoFile()
        {
            ClassDirectory root = new ClassDirectory("c:\\test");

            ClassFile file1 = root.AddFile("c:\\test\\file1", 80,  "123");
            ClassFile file2 = root.AddFile("c:\\test\\file2", 60, "123");

            Assert.AreEqual(root.Size, file1.Size + file2.Size);

        }

        [TestMethod]
        public void TestMethodSizeTwoFileOneDir()
        {
            ClassDirectory root = new ClassDirectory("c:\\test");

            ClassDirectory test2 = root.AddDirectory("c:\\test\\test2");


            ClassFile file1 = test2.AddFile("c:\\test\\test2\\file1", 80, "123");
            ClassFile file2 = test2.AddFile("c:\\test\\test2\\file2", 60, "123");

            Assert.AreEqual(root.Size, file1.Size + file2.Size);

        }

        [TestMethod]
        public void TestMethodSizeTwoFileTwoDir()
        {
            ClassDirectory root = new ClassDirectory("c:\\test");

            ClassDirectory test2 = root.AddDirectory("c:\\test\\test2");

            ClassDirectory test3 = test2.AddDirectory("c:\\test\\test2\\test3");

            ClassFile file1 = test3.AddFile("c:\\test\\test2\\test3\\file1", 80, "123");
            ClassFile file2 = test3.AddFile("c:\\test\\test2\\test3\\file2", 60, "123");

            Assert.AreEqual(root.Size, file1.Size + file2.Size);
           
        }

        [TestMethod]
        public void TestMethodSizeMultipleFileMultipleDir()
        {
            ClassDirectory root = new ClassDirectory("c:\\test");

            ClassDirectory test1 = root.AddDirectory("c:\\test\\test1");
            ClassDirectory test2 = root.AddDirectory("c:\\test\\test2");

            ClassDirectory test3 = test2.AddDirectory("c:\\test\\test2\\test3");

            ClassFile file1 = test3.AddFile("c:\\test\\test2\\test3\\file1", 80, "123");

            ClassFile file2 = test3.AddFile("c:\\test\\test2\\test3\\file2", 60, "123");
            ClassFile file3 = test3.AddFile("c:\\test\\test2\\test3\\file3", 10, "123");

            ClassFile file4 = test1.AddFile("c:\\test\\test1\\file4", 30, "123");
            ClassFile file5 = test2.AddFile("c:\\test\\test1\\file5", 50, "123");

            Assert.AreEqual(root.Size, file1.Size + file2.Size + file3.Size + file4.Size + file5.Size);

        }

    }
}

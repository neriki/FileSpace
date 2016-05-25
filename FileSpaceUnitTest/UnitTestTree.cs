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
    /// <summary>
    /// Summary description for UnitTestTree
    /// </summary>
    [TestClass]
    public class UnitTestTree
    {
        [TestMethod]
        public void TestMethodTreeOneDir()
        {
            ClassDirectory root = new ClassDirectory("c:\\test");

            root.AddDirectory("c:\\test\\test2");




            foreach (var item in root)
            {
                Assert.AreEqual(item.Name, "c:\\test\\test2");
                Assert.AreEqual(item.Type, "Dir.");
                Assert.AreEqual(item.Size, 0);
            }
            

        }
        
        [TestMethod]
        public void TestMethodTreeTwoDir()
        {
            ClassDirectory root = new ClassDirectory("c:\\test");

            root.AddDirectory("c:\\test\\test2");
            root.AddDirectory("c:\\test\\test3");

            int cpt = 0;
            foreach (var item in root)
            {
                Assert.AreEqual(item.Type, "Dir.");
                Assert.AreEqual(item.Size, 0);
                cpt++;
            }

            Assert.AreEqual(cpt, 2);
        }
        

        [TestMethod]
        public void TestMethodTreeTwoPlusOneDir()
        {
            ClassDirectory root = new ClassDirectory("c:\\test");

            root.AddDirectory("c:\\test\\test2");
            ClassDirectory test3 = root.AddDirectory("c:\\test\\test3");

            test3.AddDirectory("c:\\test\\test3\\test4");

            int cpt = 0;
            foreach (var item in root)
            {
                Assert.AreEqual(item.Type, "Dir.");
                Assert.AreEqual(item.Size, 0);
                foreach (var itm in (ClassDirectory) item)
                {
                    Assert.AreEqual(itm.Type, "Dir.");
                    Assert.AreEqual(itm.Size, 0);
                    cpt++;
                }
                cpt++;
            }

            Assert.AreEqual(cpt, 3);
        }

        [TestMethod]
        public void TestMethodTreeTwoPlusThreeDir()
        {
            ClassDirectory root = new ClassDirectory("c:\\test");

            ClassDirectory test2 = root.AddDirectory("c:\\test\\test2");
            ClassDirectory test3 = root.AddDirectory("c:\\test\\test3");

            test3.AddDirectory("c:\\test\\test3\\test4");

            test2.AddDirectory("c:\\test\\test2\\test5");

            test2.AddDirectory("c:\\test\\test2\\test6");

            int cpt = 0;
            foreach (var item in root)
            {
                Assert.AreEqual(item.Type, "Dir.");
                Assert.AreEqual(item.Size, 0);
                foreach (var itm in (ClassDirectory)item)
                {
                    Assert.AreEqual(itm.Type, "Dir.");
                    Assert.AreEqual(itm.Size, 0);
                    cpt++;
                }
                cpt++;
            }

            Assert.AreEqual(cpt, 5);
        }

    }
}

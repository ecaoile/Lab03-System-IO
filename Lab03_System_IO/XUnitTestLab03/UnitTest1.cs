using System;
using Xunit;
using System.IO;
using Lab03_System_IO;
using static Lab03_System_IO.Program;

namespace XUnitTestLab03
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(@"Test_Text_List1.txt", "file created")]
        [InlineData(@"Test_Text_List2.txt", "file created")]
        public void CanCreateNew(string testPath, string expectedResult)
        {
            Assert.Equal(expectedResult, CreateEasyFile(testPath));
            File.Delete(testPath);
        }

        [Theory]
        [InlineData("", "not a valid .txt file")]
        [InlineData("dsatkjklstdj.#@$^&&$", "not a valid .txt file")]
        public void PreventCreatingNonTxt(string testPath, string expectedResult)
        {
            Assert.Equal(expectedResult, CreateEasyFile(testPath));
        }

        [Theory]
        [InlineData(@"Test_Text_List1.txt", "file exists")]
        [InlineData(@"Test_Text_List2.txt", "file exists")]
        public void CanShowCreated(string testPath, string expectedResult)
        {
            CreateEasyFile(testPath);
            Assert.Equal(expectedResult, CreateEasyFile(testPath));
            File.Delete(testPath);
        }

        [Theory]
        [InlineData(@"Test_Text_List1.txt", "file read succesfully")]
        [InlineData(@"Test_Text_List2.txt", "file read succesfully")]
        public void CanReadFile(string testPath, string expectedResult)
        {
            CreateEasyFile(testPath);
            Assert.Equal(expectedResult, ReadFile(testPath));
            File.Delete(testPath);
        }

        [Theory]
        [InlineData(@"Test_Text_List1.txt", "bacon", "added word")]
        [InlineData(@"Test_Text_List1.txt", "hello", "word exists")]
        [InlineData(@"Test_Text_List2.txt", "world", "word exists")]
        [InlineData(@"Test_Text_List1.txt", "", "invalid word")]
        [InlineData(@"Test_Text_List2.txt", 
            "!$#@@$#@#!$@!#$@fdsjldfajsdfljkflasjfjadsjfjalsdffsdaij" +
            "foasdjoasjtioajsotjioasjdtatsadsgarqeQ", "invalid word")]
        public void CanAddWord(string testPath, string wordToAdd, string expectedResult)
        {
            CreateEasyFile(testPath);
            Assert.Equal(expectedResult, AddWord(wordToAdd, testPath));
            File.Delete(testPath);
        }

        [Theory]
        [InlineData(@"Test_Text_List1.txt", "bacon", "word not found")]
        [InlineData(@"Test_Text_List1.txt", "hello", "deleted word")]
        [InlineData(@"Test_Text_List2.txt", "world", "deleted word")]
        public void CanDeleteWord(string testPath, string wordToDelete, string expectedResult)
        {
            CreateEasyFile(testPath);
            Assert.Equal(expectedResult, DeleteWord(wordToDelete, testPath));
            File.Delete(testPath);
        }

        [Theory]
        [InlineData(@"Test_Text_List1.txt", "file deleted")]
        [InlineData(@"Test_Text_List2.txt", "file deleted")]
        public void CanDeleteFile(string testPath, string expectedResult)
        {
            CreateEasyFile(testPath);
            Assert.Equal(expectedResult, DeleteFile(testPath));
        }

        [Theory]
        [InlineData(@"", "no file to delete")]
        [InlineData(@"asdtsdtsltjjaslktj", "no file to delete")]
        public void CannotDeleteNoFile(string testPath, string expectedResult)
        {
            Assert.Equal(expectedResult, DeleteFile(testPath));
        }
    }
}

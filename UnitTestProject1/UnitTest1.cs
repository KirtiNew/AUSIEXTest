using WebApplication1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTestCommitComments
    {

        [TestMethod]
        [TestCategory("Unit Test")]
        public void CheckingNotNullCommitCommets_BasedOnCommit_ReturnsListCommitComments()
        {
            //Arrange 
            WebApplication1.Controllers.CommentController commentController = new WebApplication1.Controllers.CommentController();

            //Act  
            List<Commit> _listCommits = commentController.GetCommits();
            List<CommitComment> _commitComments = commentController.GetCommitComments(_listCommits);

            //Assert  
            Assert.IsNotNull(_commitComments);
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        public void CheckingExpectedCommitCommetsValue_BasedOnCommit_ReturnsListCommitComments()
        {
            //Arrange 
            int expectedValue = 90;
            WebApplication1.Controllers.CommentController commentController = new WebApplication1.Controllers.CommentController();

            //Act  
            List<Commit> _listCommits = commentController.GetCommits();
            List<CommitComment> _commitComments = commentController.GetCommitComments(_listCommits);

            //Assert  
            Assert.AreEqual(expectedValue, _commitComments.Count);
        }
    }
}

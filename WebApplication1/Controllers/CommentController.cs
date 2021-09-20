using WebApplication1.Constants;
using WebApplication1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;


namespace WebApplication1.Controllers
{
    public class CommentController : Controller
    {
     
        public ActionResult Index()
        {
            return View();
        }

        public  List<Commit> GetCommits()
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Authentication-Token", APIEndPoints.apiKey);
            webClient.Headers.Add("user-agent", "user1");
            List<Commit> _listCommits = new List<Commit>();
            try
            {
                string content = webClient.DownloadString(APIEndPoints.gitRepoURL);
                _listCommits = JsonConvert.DeserializeObject<List<Commit>>(content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return _listCommits;
        }

        public List<CommitComment> GetCommitComments(List<Commit> listCommits)
        {
            List<CommitComment> _list = new List<CommitComment>();
            WebClient webClient = new WebClient();
            try
            {
                if(listCommits.Count>0)
                {
                    for (int i = 0; i < listCommits.Count; i++)
                    {
                        webClient.Headers.Add("Authentication-Token", APIEndPoints.apiKey);
                        webClient.Headers.Add("user-agent", listCommits[i].sha);
                        string EndPoints = listCommits[i].sha + "/comments";
                        string URLEndPoints = APIEndPoints.gitRepoURL + "/" + EndPoints;
                        string content = webClient.DownloadString(URLEndPoints);
                        var listCommitComments = JsonConvert.DeserializeObject<List<CommitComment>>(content);
                        _list.AddRange(listCommitComments);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return _list;

        }

        [HttpGet]
        public JsonResult GetCommitCommentList()
        {
            List<Commit> _listSHA = GetCommits();
            List<CommitComment> _listCommits = GetCommitComments(_listSHA);
            var x = (from listCommits in _listCommits
                     select new CommitComment
                     {
                         commit_id = listCommits.commit_id,
                         body = listCommits.body.Trim(),
                         wordcount = listCommits.body.Trim().Length
                     }).OrderBy(a => a.wordcount).ToList();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
    }
}


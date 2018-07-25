using Akka.Actor;
using Akka.TestKit.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiReader.Actors;
using System.Threading;
using System.Diagnostics;
using Moq;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace ApiReader.Tests
{
    [TestFixture]
    class ActorsTests : TestKit
    {
        private ApiReaderViewModel viewModel;
        private ReadApiActor readApiActor;
        public void Setup_VievModel_and_ReadApiActor()
        {
            viewModel = new ApiReaderViewModel();
            readApiActor = new ReadApiActor(viewModel);
        }


        [TestCase("wojtek-rak", "SFML-Basic-Platform-Game","{'id':129069134,'node_id':'MDEwOlJlcG9zaXRvcnkxMjkwNjkxMzQ=','name':'SFML-Basic-Platform-Game','full_name':'wojtek-rak/SFML-Basic-Platform-Game','owner':{'login':'wojtek-rak','id':29315093,'node_id':'MDQ6VXNlcjI5MzE1MDkz','avatar_url':'https://avatars2.githubusercontent.com/u/29315093?v=4','gravatar_id':'','url':'https://api.github.com/users/wojtek-rak','html_url':'https://github.com/wojtek-rak','followers_url':'https://api.github.com/users/wojtek-rak/followers','following_url':'https://api.github.com/users/wojtek-rak/following{/other_user}','gists_url':'https://api.github.com/users/wojtek-rak/gists{/gist_id}','starred_url':'https://api.github.com/users/wojtek-rak/starred{/owner}{/repo}','subscriptions_url':'https://api.github.com/users/wojtek-rak/subscriptions','organizations_url':'https://api.github.com/users/wojtek-rak/orgs','repos_url':'https://api.github.com/users/wojtek-rak/repos','events_url':'https://api.github.com/users/wojtek-rak/events{/privacy}','received_events_url':'https://api.github.com/users/wojtek-rak/received_events','type':'User','site_admin':false},'private':false,'html_url':'https://github.com/wojtek-rak/SFML-Basic-Platform-Game','description':null,'fork':false,'url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game','forks_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/forks','keys_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/keys{/key_id}','collaborators_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/collaborators{/collaborator}','teams_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/teams','hooks_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/hooks','issue_events_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/issues/events{/number}','events_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/events','assignees_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/assignees{/user}','branches_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/branches{/branch}','tags_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/tags','blobs_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/blobs{/sha}','git_tags_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/tags{/sha}','git_refs_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/refs{/sha}','trees_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/trees{/sha}','statuses_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/statuses/{sha}','languages_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/languages','stargazers_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/stargazers','contributors_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/contributors','subscribers_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/subscribers','subscription_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/subscription','commits_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/commits{/sha}','git_commits_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/commits{/sha}','comments_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/comments{/number}','issue_comment_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/issues/comments{/number}','contents_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/contents/{+path}','compare_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/compare/{base}...{head}','merges_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/merges','archive_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/{archive_format}{/ref}','downloads_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/downloads','issues_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/issues{/number}','pulls_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/pulls{/number}','milestones_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/milestones{/number}','notifications_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/notifications{?since,all,participating}','labels_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/labels{/name}','releases_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/releases{/id}','deployments_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/deployments','created_at':'2018-04-11T09:29:13Z','updated_at':'2018-04-24T23:06:12Z','pushed_at':'2018-04-24T23:06:11Z','git_url':'git://github.com/wojtek-rak/SFML-Basic-Platform-Game.git','ssh_url':'git@github.com:wojtek-rak/SFML-Basic-Platform-Game.git','clone_url':'https://github.com/wojtek-rak/SFML-Basic-Platform-Game.git','svn_url':'https://github.com/wojtek-rak/SFML-Basic-Platform-Game','homepage':null,'size':4531,'stargazers_count':0,'watchers_count':0,'language':'C++','has_issues':true,'has_projects':true,'has_downloads':true,'has_wiki':true,'has_pages':false,'forks_count':0,'mirror_url':null,'archived':false,'open_issues_count':0,'license':null,'forks':0,'open_issues':0,'watchers':0,'default_branch':'master','network_count':0,'subscribers_count':1}")]
        [TestCase("wojtek-rak", "Definitely Invalid Repname","404")]
        public void ReadApiActor_GetHTMLStringTest(String userName, string repName, String correct)
        {
            Setup_VievModel_and_ReadApiActor();
            Assert.AreEqual(correct, readApiActor.GetHtmlString(userName, repName).Replace('"', '\''));
        }
        [TestCase(true, "{'id':129069134,'node_id':'MDEwOlJlcG9zaXRvcnkxMjkwNjkxMzQ=','name':'SFML-Basic-Platform-Game','full_name':'wojtek-rak/SFML-Basic-Platform-Game','owner':{'login':'wojtek-rak','id':29315093,'node_id':'MDQ6VXNlcjI5MzE1MDkz','avatar_url':'https://avatars2.githubusercontent.com/u/29315093?v=4','gravatar_id':'','url':'https://api.github.com/users/wojtek-rak','html_url':'https://github.com/wojtek-rak','followers_url':'https://api.github.com/users/wojtek-rak/followers','following_url':'https://api.github.com/users/wojtek-rak/following{/other_user}','gists_url':'https://api.github.com/users/wojtek-rak/gists{/gist_id}','starred_url':'https://api.github.com/users/wojtek-rak/starred{/owner}{/repo}','subscriptions_url':'https://api.github.com/users/wojtek-rak/subscriptions','organizations_url':'https://api.github.com/users/wojtek-rak/orgs','repos_url':'https://api.github.com/users/wojtek-rak/repos','events_url':'https://api.github.com/users/wojtek-rak/events{/privacy}','received_events_url':'https://api.github.com/users/wojtek-rak/received_events','type':'User','site_admin':false},'private':false,'html_url':'https://github.com/wojtek-rak/SFML-Basic-Platform-Game','description':null,'fork':false,'url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game','forks_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/forks','keys_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/keys{/key_id}','collaborators_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/collaborators{/collaborator}','teams_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/teams','hooks_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/hooks','issue_events_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/issues/events{/number}','events_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/events','assignees_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/assignees{/user}','branches_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/branches{/branch}','tags_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/tags','blobs_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/blobs{/sha}','git_tags_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/tags{/sha}','git_refs_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/refs{/sha}','trees_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/trees{/sha}','statuses_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/statuses/{sha}','languages_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/languages','stargazers_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/stargazers','contributors_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/contributors','subscribers_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/subscribers','subscription_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/subscription','commits_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/commits{/sha}','git_commits_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/git/commits{/sha}','comments_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/comments{/number}','issue_comment_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/issues/comments{/number}','contents_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/contents/{+path}','compare_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/compare/{base}...{head}','merges_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/merges','archive_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/{archive_format}{/ref}','downloads_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/downloads','issues_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/issues{/number}','pulls_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/pulls{/number}','milestones_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/milestones{/number}','notifications_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/notifications{?since,all,participating}','labels_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/labels{/name}','releases_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/releases{/id}','deployments_url':'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/deployments','created_at':'2018-04-11T09:29:13Z','updated_at':'2018-04-24T23:06:12Z','pushed_at':'2018-04-24T23:06:11Z','git_url':'git://github.com/wojtek-rak/SFML-Basic-Platform-Game.git','ssh_url':'git@github.com:wojtek-rak/SFML-Basic-Platform-Game.git','clone_url':'https://github.com/wojtek-rak/SFML-Basic-Platform-Game.git','svn_url':'https://github.com/wojtek-rak/SFML-Basic-Platform-Game','homepage':null,'size':4531,'stargazers_count':0,'watchers_count':0,'language':'C++','has_issues':true,'has_projects':true,'has_downloads':true,'has_wiki':true,'has_pages':false,'forks_count':0,'mirror_url':null,'archived':false,'open_issues_count':0,'license':null,'forks':0,'open_issues':0,'watchers':0,'default_branch':'master','network_count':0,'subscribers_count':1}")]
        [TestCase(false,"InvalidHTMLString")]
        public void ReadApiActor_ParseToJsonTest(bool answer,String correct)
        {
            Setup_VievModel_and_ReadApiActor();
            bool result = true;
            try
            {
               String test = readApiActor.ParseToJsonString(correct);
            }
            catch
            {
                result = false;
            }
            Assert.AreEqual(answer, result);
        }

        [TestCase("{'full_name': 'wojtek-rak/SFML-Basic-Platform-Game','description': null,'languages_url': 'https://api.github.com/repos/wojtek-rak/SFML-Basic-Platform-Game/languages','created_at': '11.04.2018 09:29:13','clone_url': 'https://github.com/wojtek-rak/SFML-Basic-Platform-Game.git','watchers_count': 0,'language': 'C++','mirror_url': null,'open_issues_count': 0,'watchers': 0 }")]
        public void ReadApiActor_ConvertToJobjectTest(String parsedJsonString)
        {
            //Mock<IReadApiActor> name = new Mock<IReadApiActor>();
            //name.CallBase = true;
            //name.Setup(x => x.GetHtmlString(null, null)).Returns("");
            //name.Setup(x => x.ParseToJsonString(null)).Returns(parsedJsonString);
            Setup_VievModel_and_ReadApiActor();
            var results = readApiActor.ConvertToJobject(parsedJsonString);
            var expected = typeof(Newtonsoft.Json.Linq.JObject);
            Assert.IsTrue(Type.Equals(results.GetType(), expected));
        }

        [Test]
        public void ReadApiActor_InputToTableTest()
        {
            Mock<ApiReaderViewModel> viewModel = new Mock<ApiReaderViewModel>();
            viewModel.CallBase = true;
            viewModel.Setup(x => x.CreateApiTable()).Verifiable();

            readApiActor = new ReadApiActor(viewModel.Object);
            JObject jObject = new JObject();

            readApiActor.InputToTabel(jObject);
            var result = viewModel.Object.ApiJObjectToTable;
            var expected = jObject;
            
            Assert.AreEqual(expected, result);
        }

    }
}

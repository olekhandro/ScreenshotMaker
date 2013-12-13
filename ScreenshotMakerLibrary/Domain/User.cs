using System.Collections;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace ScreenshotMakerLibrary.Domain
{
    public class User:Identifiable
    {
        private  ISet<Project> _projects = new HashedSet<Project>();

        public string Username { get; set; }
        public string Password { get; set; }

        public ISet<Project> Projects
        {
            get
            {
                return _projects;
            }
            set { _projects = value; }
        }

        protected User()
        {
        }

        public override string ToString()
        {
            return Username;
        }
    }
}
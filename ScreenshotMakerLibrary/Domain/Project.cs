using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace ScreenshotMakerLibrary.Domain
{
    public class Project : Identifiable
    {

        private ISet<User> _users = new HashedSet<User>();

        public string ProjectTitle { get; set; }
        public string Company { get; set; }

        public ISet<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        protected Project()
        {
            
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Company, ProjectTitle);
        }
    }
}

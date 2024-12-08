using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;

namespace BlackFriday.Models
{
    public abstract class User : IUser
    {
        private string userName;
        private bool hasDataAccess;
        private string email;

        public User(string userName, string email, bool hasDataAccess)
        {
            this.userName = userName;
            Email = email;
            HasDataAccess = hasDataAccess;
        }
        public string UserName => this.userName;
        //{
        //    get => this.userName;
        //    private set
        //    {
        //        if (string.IsNullOrWhiteSpace(value))
        //        {
        //            throw new ArgumentException(string.Format(ExceptionMessages.UserNameRequired));
        //        }
        //        this.userName = value;
        //    }
        //}

        public bool HasDataAccess
        {
            get => this.hasDataAccess;
            private set
            {
                this.hasDataAccess = value;
            }
        }

        public string Email
        {
            get
            {
                if (HasDataAccess)
                {
                    return "hidden";
                }
                return this.email;
            }
            private set
            {
                if (!HasDataAccess && string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.EmailRequired));
                }
                this.email = value;
            }
        }

        public override string ToString()
        {
            return $"{this.UserName} - Status: {this.GetType().Name}, Contact Info: {Email}";
        }
    }
}

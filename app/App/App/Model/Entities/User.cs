namespace App.Model.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public int UserTypeId { get; set; }
        
        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Email { get; set; }


        public string Telephone { get; set; }

        public string ImagePath { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "noimage";
                }

                return string.Format(
                    "https://apppruebalanapi.azurewebsites.net/{0}",
                    ImagePath.Substring(1)
                );
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);

            }
        }

        public byte[] ImageArray { get; internal set; }

        public string Password { get; internal set; }
    }
}

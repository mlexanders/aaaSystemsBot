namespace aaaTgBot.Data.Models
{
    public class RegistrationModel
    {
        public long ChatId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }

        public bool NameIsValid()
        {
            if (string.IsNullOrEmpty(Name)) return false;

            foreach (var c in Name)
                if (!char.IsLetter(c) || c.Equals(' ')) return false;

            return true;
        }

        public bool PhoneIsValid()
        {
            if (string.IsNullOrEmpty(Phone)) return false;
            if (Phone.Length < 10) return false;

            var validСharacters = new char[] { '+', '-', '(', ')', ' ' };

            foreach (var c in Phone)
                if (!char.IsDigit(c) & !validСharacters.Contains(c)) return false;

            return true;
        }
    }
}
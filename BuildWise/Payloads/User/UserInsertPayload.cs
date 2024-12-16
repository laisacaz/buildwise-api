namespace BuildWise.Payload.User
{
    public class UserInsertPayload
    {
        private string _registeredNumber;
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public string RegisteredNumber
        {
            get => _registeredNumber;
            set => _registeredNumber = RemoveNonNumericCharacters(value);
        }
        private string RemoveNonNumericCharacters(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}

namespace CrackerZIPArchiveWithPassword
{
    class PasswordIncrementor
    {
        public static string IncrementPassword(string password, int index)
        {
            if (password[index].Equals('9'))
            {
                password = ChangeCharInPassword(password, index, 'a');
                return password;
            }
            if (password[index].Equals('z'))
            {
                password = ChangeCharInPassword(password, index, 'A');
                return password;
            }
            if (password[index].Equals('Z'))
            {
                if (index == 0)
                {
                    string str = password.Insert(index, '0'.ToString());
                    password = str;
                    password = ChangeCharInPassword(password, index + 1, '0');
                    return password;
                }
                else
                {
                    password = ChangeCharInPassword(password, index, '0');
                    password = IncrementPassword(password, index - 1);
                    return password;
                }
            }
            else
            {
                char symb = password[index];
                symb++;
                password = ChangeCharInPassword(password, index, symb);
                return password;
            }
        }

        private static string ChangeCharInPassword(string password, int index, char symbol)
        {
            if (string.IsNullOrEmpty(password) || index < 0 || index >= password.Length) return password;

            string password1 = password.Insert(index, symbol.ToString());
            string password2 = password1.Remove(index + 1, 1);
            return password2;
        }
    }
}
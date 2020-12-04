using System.Linq;

namespace CodeAdvent
{
    struct Password
    {
        readonly string text;
        readonly char ruleChar;
        readonly int upperBound;
        readonly int lowerBound;

        public Password(string input)
        {
            string[] splitText = input.Split(':');
            string rule = splitText[0];
            text = splitText[1].Trim();

            string[] ruleNums = rule.Split("-");
            lowerBound = int.Parse(ruleNums[0]);

            string[] upperAndChar = ruleNums[1].Split(' ');
            upperBound = int.Parse(upperAndChar[0]);
            ruleChar = upperAndChar[1].ToCharArray()[0];    
        }

        public bool Valid()
        {
            char countChar = ruleChar;
            int charAmt = text.ToCharArray().Where(c => c == countChar).Count();
            return charAmt <= upperBound && charAmt >= lowerBound;
        }

        public bool Valid2()
        {
            return false;
        }
    }
}

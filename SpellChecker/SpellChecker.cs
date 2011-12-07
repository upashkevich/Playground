    /// <summary>
    /// Implemetation of spell checker in C#. This implementation is based on Peter Norvig's article - http://norvig.com/spell-correct.html
    /// Also, this implementation was highly influenced by Lorenzo Stoakes article - http://www.codegrunt.co.uk/2010/11/02/C-Sharp-Norvig-Spelling-Corrector.html
    /// </summary>
    public class SpellChecker
    {
        private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";

        private const string WORD_REGEX = "[a-z]+";

        private Dictionary<string, int> wordsCount;

        /// <summary>
        /// Creates spell checker instance based on dictionary file data
        /// </summary>
        /// <param name="fileName">Path to dictionary file. I used big.txt, which can be found on norvig.com, for tests.</param>
        public SpellChecker(string fileName)
        {
            this.wordsCount = (from Match m in Regex.Matches(File.ReadAllText(fileName).ToLower(), WORD_REGEX)
                               group m.Value by m.Value)
                                   .ToDictionary(gr => gr.Key, gr => gr.Count());
        }

        /// <summary>
        /// Returns the best found correction for a given string
        /// </summary>
        /// <param name="str">Raw string</param>
        /// <returns>Best found correction</returns>
        public string GetCorrection(string str)
        {
            var candidates = Known(new List<string> { str });

            if (candidates.Count() == 0)
            {
                candidates = KnownEdits1(str);
            }

            if (candidates.Count() == 0)
            {
                candidates = KnownEdits2(str);
            }

            if (candidates.Count() == 0)
            {
                candidates = new List<string> { str };
            }

            return (from candidate in candidates
                    orderby (wordsCount.ContainsKey(candidate) ? wordsCount[candidate] : 0) descending
                    select candidate).First();
        }

        private IEnumerable<string> Edits1(string str)
        {
            var delitionEdits = from i in Enumerable.Range(0, str.Length)
                                select str.Substring(0, i) + str.Substring(i + 1);

            var transpositionEdits = from i in Enumerable.Range(0, str.Length - 1)
                                     select str.Substring(0, i) + str.Substring(i + 1, 1) +
                                            str.Substring(i, 1) + str.Substring(i + 2);

            var alternationsEdits = from i in Enumerable.Range(0, str.Length)
                                    from c in ALPHABET
                                    select str.Substring(0, i) + c + str.Substring(i + 1);

            var insertionEdits = from i in Enumerable.Range(0, str.Length + 1)
                                 from c in ALPHABET
                                 select str.Substring(0, i) + c + str.Substring(i);

            return delitionEdits.Union(transpositionEdits).Union(alternationsEdits).Union(insertionEdits);
        }

        private IEnumerable<string> Known(IEnumerable<string> words)
        {
            return words.Where(w => wordsCount.ContainsKey(w));
        }

        private IEnumerable<string> KnownEdits1(string str)
        {
            return Known(Edits1(str));
        }

        private IEnumerable<string> KnownEdits2(string str)
        {
            return (from e1 in Edits1(str)
                    from e2 in Edits1(e1)
                    where wordsCount.ContainsKey(e2)
                    select e2).Distinct();
        }
    }
    public static class BoyerMoore
    {
        private static int ALPHABET_LEN = 255;

        private static int[] delta1;

        private static int[] delta2;

        private static void FillDelta1(string needle)
        {
            for (int i = 0; i < ALPHABET_LEN; i++)
            {
                delta1[i] = needle.Length;
            }

            for (int i = 0; i < needle.Length - 1; i++)
            {
                delta1[needle[i]] = needle.Length - 1 - i;
            }
        }

        // checks if str[p .. str.Length - 1] is prefix of the string str
        private static bool IsPrefix(string word, int pos)
        {
            var suffixLen = word.Length - pos;

            for (int i = 0; i < suffixLen; i++)
            {
                if (word[i] != word[pos + i])
                {
                    return false;
                }
            }

            return true;
        }

        // returns the length of the longest common suffix of str and str[1 .. p]
        private static int GetSuffixLength(string word, int pos)
        {
            var i = 0;

            while (i < pos && word[pos - i] == word[word.Length - 1 - i])
            {
                i++;
            }

            return i;
        }

        private static void FillDelta2(string needle)
        {
            var lastPrefixIndex = needle.Length;

            for (int i = needle.Length - 1; i >= 0; i--)
            {
                if (IsPrefix(needle, i + 1))
                {
                    lastPrefixIndex = i + 1;
                }

                delta2[i] = lastPrefixIndex;
            }

            for (int i = 0; i < needle.Length - 1; i++)
            {
                var suffixLength = GetSuffixLength(needle, i);

                if (needle[i - suffixLength] != needle[needle.Length - 1 - suffixLength])
                {
                    delta2[needle.Length - 1 - suffixLength] = needle.Length - 1 - i;
                }
            }
        }

        public static int Match(string haystack, string needle)
        {
            delta1 = new int[ALPHABET_LEN];
            delta2 = new int[needle.Length];
            FillDelta1(needle);
            FillDelta2(needle);
            var i = needle.Length - 1;

            while (i < haystack.Length)
            {
                var j = needle.Length - 1;

                while (j >= 0 && haystack[i] == needle[j])
                {
                    i--;
                    j--;
                }

                if (j < 0)
                {
                    FreeMemory();
                    return i + 1;
                }

                i += Math.Max(delta1[haystack[i]], delta2[j]);
            }

            FreeMemory();
            return -1;
        }

        private static void FreeMemory()
        {
            delta1 = null;
            delta2 = null;
        }
    }
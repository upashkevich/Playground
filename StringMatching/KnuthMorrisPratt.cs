    public static class KnuthMorrisPratt
    {
        private static int[] t;

        private static void BuildTable(string needle)
        {
            t[0] = -1;

            if (needle.Length > 1)
            {
                t[1] = 0;
            }

            if (needle.Length > 2)
            {
                var position = 2;
                var candidate = 0;

                while (position < needle.Length)
                {
                    if (needle[position - 1] == needle[candidate])
                    {
                        candidate++;
                        t[position] = candidate;
                        position++;
                    }
                    else if (candidate > 0)
                    {
                        candidate = t[candidate];
                    }
                    else
                    {
                        t[position] = 0;
                        position++;
                    }
                }
            }
        }

        public static int Match(string haystack, string needle)
        {
            if (needle.Length == 0)
            {
                return -1;
            }

            t = new int[needle.Length];
            BuildTable(needle);

            var i = 0;
            var j = 0;

            while (i + j < haystack.Length)
            {
                if (haystack[i + j] == needle[j])
                {
                    if (j == needle.Length - 1)
                    {
                        return i;
                    }
                    else
                    {
                        j++;
                    }
                }
                else
                {
                    i += j - t[j];
                    j = t[j] > 0 ? t[j] : 0;
                }
            }

            return -1;
        }
    }
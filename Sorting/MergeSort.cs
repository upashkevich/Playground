    /// <summary>
    /// merge sort implementation
    /// </summary>
    public static class MergeSort
    {
        // supplemental array for merging
        private static int[] b;

        public static void Sort(int[] a)
        {
            b = new int[a.Length];
            Sort(a, 0, a.Length - 1);
            b = null;
        }

        private static void Sort(int[] a, int left, int right)
        {
            if (right - left < 1)
            {
                // no need to sort further
                return;
            }

            var mid = left + (right - left) / 2;
            Sort(a, left, mid);
            Sort(a, mid + 1, right);
            Merge(a, left, mid, right);
        }

        private static void Merge(int[] a, int left, int mid, int right)
        {
            var i = left;
            var j = mid + 1;
            var k = left;

            while (i <= mid && j <= right)
            {
                if (a[i] < a[j])
                {
                    b[k] = a[i];
                    i++;
                }
                else
                {
                    b[k] = a[j];
                    j++;
                }

                k++;
            }

            while (i <= mid)
            {
                b[k] = a[i];
                i++;
                k++;
            }

            while (j <= right)
            {
                b[k] = a[j];
                j++;
                k++;
            }

            for (int p = left; p <= right; p++)
            {
                a[p] = b[p];
            }
        }
    }
    /// <summary>
    /// In-place qsort implementation with random pivot
    /// </summary>
    public static class QSort
    {
        private static Random random = new Random(DateTime.Now.Second);

        public static void Sort(int[] a)
        {
            QuickSort(a, 0, a.Length - 1);
        }

        private static void QuickSort(int[] a, int left, int right)
        {
            if (right - left <= 1)
            {
                return;
            }

            // select random pivot
            var pivotIndex = random.Next(left, right);
            Swap(ref a[left], ref a[pivotIndex]);
            var pivot = a[left];
            var oldLeft = left;
            var oldRight = right;

            while (left < right)
            {
                while (left < right && a[right] >= pivot)
                {
                    right--;
                }

                if (left != right)
                {
                    Swap(ref a[left], ref a[right]);
                    left++;
                }

                while (left < right && a[left] <= pivot)
                {
                    left++;
                }

                if (left != right)
                {
                    Swap(ref a[left], ref a[right]);
                    right--;
                }
            }

            a[left] = pivot;
            QuickSort(a, oldLeft, left - 1);
            QuickSort(a, left + 1, oldRight);
        }

        private static void Swap(ref int x, ref int y)
        {
            var temp = x;
            x = y;
            y = temp;
        }
    }
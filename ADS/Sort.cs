class Sort {

    private static T FindMaxEl<T> (T[] list) where T : IComparable
    {
        T max = list[0];
        foreach(T el in list) {
            if (el.CompareTo(max) > 0) {
                max = el;
            }
        }
        return max;
    }

    private static T FindMinEl<T> (T[] list, int limit) where T : IComparable
    {
        T min = list[0];
        foreach(T el in list) {
            if (el.CompareTo(min) < 0) {
                min = el;
            }
        }
        return min;
    }
    
    public static void BubbleSort<T>(T[] list) where T : IComparable
    {
        int N = list.Length;
        int numberOfPairs = N;
        bool swappedElements = true;
        while (swappedElements)
        {
            numberOfPairs--;  // numberOfPairs = numberOfPairs - l;
            swappedElements = false;
            for (int i = 0; i < numberOfPairs; i++)
            {
                if (list[i].CompareTo(list[i + 1]) > 0) // if(list[i]>list[i+1])
                {
                    T tmp;
                    tmp = list[i]; 
                    list[i] = list[i + 1];
                    list[i + 1] = tmp; //Swap(list[i],list[i+1])
                    swappedElements = true;
                }
            }
        }
    }

    public static void MaxElemSort<T>(T[] list) where T : IComparable
    {
        int n = list.Length;
        
        while (n > 0) {
            int maxIndex = 0;
            T maxElem = list[maxIndex];
            for (int i = 0; i < n; i++) {
                if (list[i].CompareTo(maxElem) > 0) {
                    maxElem = list[i];
                    maxIndex = i;
                }
            }

            T tmp = list[n - 1]; 
            list[n - 1] = list[maxIndex];
            list[maxIndex] = tmp;
            n--;
        }

    }

    public static void InsertionSort<T>(T[] array) where T : IComparable
    {
        int n = array.Length;

        for (int step = 1; step < n; step++) {
            T key = array[step];
            int j = step - 1;

            
            while (j >= 0 && (key.CompareTo(array[j]) < 0)) {
                array[j + 1] = array[j];
                --j;
            }

            array[j + 1] = key;
        }

    }

    public static void ShellSort<T>(T[] array) where T : IComparable
    {
        int n = array.Length;

        for (int interval = n / 2; interval > 0; interval /= 2)
        {
            for (int i = interval; i < n; i++)
            {
                var currentKey = array[i];
                var k = i;
                while (k >= interval && (currentKey.CompareTo( array[k - interval] ) < 0 ))
                {
                    array[k] = array[k - interval];
                    k -= interval;
                }
                array[k] = currentKey;
            }
        }
    }

    private static void CountSort(int[] arr, int size, int exp)
    {
        int[] result = new int[size];
        int[] count = new int[10];

        for (int i = 0; i < 10; i++)
        {
            count[i] = 0;
        }

        for (int i = 0; i < size; i++)
        {
            count[(arr[i] / exp) % 10]++;
        }

        // Сдвиг обратно для выравнивания
        for (int i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
        }

        for (int i = size - 1; i >= 0; i--)
        {
            result[count[(arr[i] / exp) % 10] - 1] = arr[i];
            count[(arr[i] / exp) % 10]--;
        }

        for (int i = 0; i < size; i++)
        {
            arr[i] = result[i];
        }
    }
    
    public static void RadixSort (int[] arr, int size)
    {
        int max = FindMaxEl(arr);

        for (int exp = 1; max / exp > 0; exp *= 10)
        {
            CountSort(arr, size, exp);
        }
    }

    // quick sort
    public static void Quicksort<T> (T[] array, int leftIndex, int rightIndex) where T : IComparable
    {
        int i = leftIndex;
        int j = rightIndex;
        T pivot = array[leftIndex];
        
        while (i <= j)
        {
            while (pivot.CompareTo(array[i]) > 0) //(array[i] < pivot)
                i++;
            
            while (array[j].CompareTo(pivot) > 0) // (array[j] > pivot)
                j--;
            
            if (i <= j)
            {
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                i++;
                j--;
            }
        }
        
        if (leftIndex < j)
            Quicksort(array, leftIndex, j);
        
        if (i < rightIndex)
            Quicksort(array, i, rightIndex);
    }

    // Recursive merge sort 
    private static void MergeSortInner<T> (T[] array, int left, int middle, int right) where T : IComparable
    {
        int leftArrayLength = middle - left + 1;
        int rightArrayLength = right - middle;
        T[] leftTempArray = new T[leftArrayLength];
        T[] rightTempArray = new T[rightArrayLength];
        int i, j;
        int k = left;
        
        for (i = 0; i < leftArrayLength; ++i)
            leftTempArray[i] = array[left + i];
        
        for (j = 0; j < rightArrayLength; ++j)
            rightTempArray[j] = array[middle + 1 + j];
        
        i = 0;
        j = 0;
        
        while (i < leftArrayLength && j < rightArrayLength)
        {
            if (rightTempArray[j].CompareTo(leftTempArray[i]) >= 0) // (leftTempArray[i] <= rightTempArray[j])
                array[k++] = leftTempArray[i++];
            else
                array[k++] = rightTempArray[j++];
        }
        
        while (i < leftArrayLength)
            array[k++] = leftTempArray[i++];
        
        while (j < rightArrayLength)
            array[k++] = rightTempArray[j++];
    }

    public static void MergeSortR<T> (T[] array, int left, int right) where T : IComparable
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;
            MergeSortR(array, left, middle);
            MergeSortR(array, middle + 1, right);
            MergeSortInner(array, left, middle, right);
        }
        return;
    }

    // Нерекурсивная сортировка слиянием (merge sort)
    public static void MergeSort<T> (T []arr, int n) where T : IComparable
    {
        int curr_size;
        int left_start;

        // Merge subarrays in bottom up
        // manner. First merge subarrays
        // of size 1 to create sorted
        // subarrays of size 2, then merge
        // subarrays of size 2 to create
        // sorted subarrays of size 4, and
        // so on.
        for (curr_size = 1; curr_size <= n - 1; curr_size = 2 * curr_size)
        {
            // Pick starting point of different
            // subarrays of current size
            for (left_start = 0; left_start < n - 1; left_start += 2 * curr_size)
            {
                // Find ending point of left
                // subarray. mid+1 is starting
                // point of right
                int mid = Math.Min(left_start + curr_size - 1, n - 1);
                int right_end = Math.Min(left_start + 2 * curr_size - 1, n - 1);

                // Merge Subarrays arr[left_start...mid]
                // & arr[mid+1...right_end]
                MergeSortInner(arr, left_start, mid, right_end);
            }
        }
    }
}
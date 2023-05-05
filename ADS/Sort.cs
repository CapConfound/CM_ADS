class Sort {

    private static T FindMaxEl<T> (T[] list, int limit) where T : IComparable
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

    public static void RadixSort (int[] arr)
    {
        int i, j;
        int[] tmp = new int[arr.Length];
        for (int shift = 31; shift > -1; --shift)
        {
            j = 0;
            for (i = 0; i < arr.Length; ++i)
            {
                bool move = (arr[i] << shift) >= 0;
                if (shift == 0 ? !move : move)   
                    arr[i-j] = arr[i];
                else                             
                    tmp[j++] = arr[i];
            }
            Array.Copy(tmp, 0, arr, arr.Length-j, j);
        }

    }

    // TODO: quick sort
    public static void Quicksort<T> (T[] array, int leftIndex, int rightIndex) where T : IComparable
    {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = array[leftIndex];
        while (i <= j)
        {
            while (pivot.CompareTo(array[i]) > 0) //(array[i] < pivot)
            {
                i++;
            }
            
            while (array[j].CompareTo(pivot) > 0) // (array[j] > pivot)
            {
                j--;
            }
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
        return;
    }

    // TODO: recursive merge sort 
    private static void MergeSortInner<T> (T[] array, int left, int middle, int right) where T : IComparable
    {
        int leftArrayLength = middle - left + 1;
        int rightArrayLength = right - middle;
        T[] leftTempArray = new T[leftArrayLength];
        T[] rightTempArray = new T[rightArrayLength];
        int i, j;
        for (i = 0; i < leftArrayLength; ++i)
            leftTempArray[i] = array[left + i];
        for (j = 0; j < rightArrayLength; ++j)
            rightTempArray[j] = array[middle + 1 + j];
        i = 0;
        j = 0;
        int k = left;
        while (i < leftArrayLength && j < rightArrayLength)
        {
            if (rightTempArray[j].CompareTo(leftTempArray[i]) >= 0) // (leftTempArray[i] <= rightTempArray[j])
            {
                array[k++] = leftTempArray[i++];
            } else {
                array[k++] = rightTempArray[j++];
            }
        }
        while (i < leftArrayLength)
        {
            array[k++] = leftTempArray[i++];
        }
        while (j < rightArrayLength)
        {
            array[k++] = rightTempArray[j++];
        }
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

    //recursiveless merge sort
    public static void MergeSort<T> (T []arr, int n) where T : IComparable
    {
          
        // For current size of subarrays to
        // be merged curr_size varies from
        // 1 to n/2
        int curr_size;
                      
        // For picking starting index of
        // left subarray to be merged
        int left_start;
                          
          
        // Merge subarrays in bottom up
        // manner. First merge subarrays
        // of size 1 to create sorted
        // subarrays of size 2, then merge
        // subarrays of size 2 to create
        // sorted subarrays of size 4, and
        // so on.
        for (curr_size = 1; curr_size <= n-1;
                      curr_size = 2*curr_size)
        {
              
            // Pick starting point of different
            // subarrays of current size
            for (left_start = 0; left_start < n-1;
                        left_start += 2*curr_size)
            {
                // Find ending point of left
                // subarray. mid+1 is starting
                // point of right
                int mid = Math.Min(left_start + curr_size - 1,n-1);
          
                int right_end = Math.Min(left_start
                             + 2*curr_size - 1, n-1);
          
                // Merge Subarrays arr[left_start...mid]
                // & arr[mid+1...right_end]
                Merge(arr, left_start, mid, right_end);
            }
        }
    }
      
    /* Function to merge the two haves arr[l..m] and
    arr[m+1..r] of array arr[] */
    public static void Merge<T> (T []arr, int l, int m, int r) where T : IComparable
    {
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
      
        /* create temp arrays */
        T []L = new T[n1];
        T []R = new T[n2];
      
        /* Copy data to temp arrays L[]
        and R[] */
        for (i = 0; i < n1; i++)
            L[i] = arr[l + i];
        for (j = 0; j < n2; j++)
            R[j] = arr[m + 1+ j];
      
        /* Merge the temp arrays back into
        arr[l..r]*/
        i = 0;
        j = 0;
        k = l;
        while (i < n1 && j < n2)
        {
            if (L[i].CompareTo(R[j]) <= 0) //(L[i] <= R[j])
            {
                arr[k] = L[i];
                i++;
            }
            else
            {
                arr[k] = R[j];
                j++;
            }
            k++;
        }
      
        /* Copy the remaining elements of
        L[], if there are any */
        while (i < n1)
        {
            arr[k] = L[i];
            i++;
            k++;
        }
      
        /* Copy the remaining elements of
        R[], if there are any */
        while (j < n2)
        {
            arr[k] = R[j];
            j++;
            k++;
        }
    }
    
    
}
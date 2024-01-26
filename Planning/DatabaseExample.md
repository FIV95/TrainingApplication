### Example of Coaches Table

<pre><div class="bg-black rounded-md"><div class="p-4 overflow-y-auto"><code class="!whitespace-pre hljs language-plaintext">Coach ID: 1
First Name: Jane
Last Name: Doe
Email: jane.doe@example.com
Password: $2y$12$... (hashed)
CreatedAt: 2024-01-01 09:00:00
UpdatedAt: 2024-01-02 10:00:00
</code></div></div></pre>

### Example of Clients Table

<pre><div class="bg-black rounded-md"><div class="p-4 overflow-y-auto"><code class="!whitespace-pre hljs language-plaintext">Client ID: 10
First Name: John
Last Name: Smith
Email: john.smith@example.com
Password: $2y$12$... (hashed)
Coach ID: 1
CreatedAt: 2024-01-03 11:00:00
UpdatedAt: 2024-01-04 12:00:00
</code></div></div></pre>

### Example of Exercises Table

<pre><div class="bg-black rounded-md"><div class="p-4 overflow-y-auto"><code class="!whitespace-pre hljs language-plaintext">Exercise ID: 100
Name: Squat
Description: A lower body exercise targeting the quadriceps, hamstrings, and glutes.
Instructional Video Link: http://example.com/videos/squat
CreatedAt: 2024-01-05 13:00:00
UpdatedAt: 2024-01-06 14:00:00
</code></div></div></pre>

### Example of TrainingSessions Table

<pre><div class="bg-black rounded-md"><div class="p-4 overflow-y-auto"><code class="!whitespace-pre hljs language-plaintext">Training Session ID: 1000
Coach ID: 1
Client ID: 10
Due Date: 2024-01-10
CreatedAt: 2024-01-07 15:00:00
UpdatedAt: 2024-01-08 16:00:00
</code></div></div></pre>

### Example of Comments Table

<pre><div class="bg-black rounded-md"><div class="p-4 overflow-y-auto"><code class="!whitespace-pre hljs language-plaintext">Comment ID: 10000
Content: Great progress on your last session!
Author ID: 1 (assuming the coach made the comment)
Related Entity ID: 1000 (assuming the comment is related to a Training Session)
CreatedAt: 2024-01-09 17:00:00
UpdatedAt: 2024-01-09 18:00:00
</code></div></div></pre>

### Example of WeeklySummaries Table

<pre><div class="bg-black rounded-md"><div class="p-4 overflow-y-auto"><code class="!whitespace-pre hljs language-plaintext">Summary ID: 2000
Client ID: 10
Coach ID: 1
Week Starting Date: 2024-01-07
Summary Details: This week we'll focus on building leg strength with squats and deadlifts.
CreatedAt: 2024-01-06 19:00:00
UpdatedAt: 2024-01-06 20:00:00
</code></div></div></pre>

### Example of TrainingSessionExercises Table

<pre><div class="bg-black rounded-md"><div class="p-4 overflow-y-auto"><code class="!whitespace-pre hljs language-plaintext">Training Session Exercise ID: 3000
Training Session ID: 1000
Exercise ID: 100
Prescribed Repetitions: 10
Prescribed Sets: 3
Prescribed Weight: 100 lbs
Session Notes From Coach: "Last Set get as many reps as you can within reason"
Actual Repetitions: 10
Actual Sets: 3
Actual Weight Used: 95 lbs
Comments/Notes From User: "These felt Great!".
CreatedAt: 2024-01-10 21:00:00
UpdatedAt: 2024-01-10 22:00:00
</code></div></div></pre>


### Example Entry in TrainingSessionExerciseSets Table

<pre><div class="bg-black rounded-md"><div class="flex items-center relative text-gray-200 bg-gray-800 dark:bg-token-surface-primary px-4 py-2 text-xs font-sans justify-between rounded-t-md"><span></span><span class="" data-state="closed"></span></div></div></pre>

```plaintext
Set ID: 5000
Training Session Exercise ID: 3000
Set Number: 1
Repetitions Achieved: 10
Weight Used: 95 lbs
Comments/Notes: "Felt good"
CreatedAt: 2024-01-11 09:00:00
UpdatedAt: 2024-01-11 09:15:00
```


### Example of PerformanceRecords Table

```plaintext
Record ID: 4000
Client ID: 10
Exercise ID: 100
Tonnage: 2850 lbs (calculated as 95 lbs * 10 repetitions * 3 sets)
Most Weight Lifted: 105 lbs
Most Weight Lifted Date: 2024-01-10
Comments/Notes: Personal best on squat weight.
CreatedAt: 2024-01-11 23:00:00
UpdatedAt: 2024-01-11 23:30:00
```

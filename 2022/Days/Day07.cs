using Common;

namespace _2022.Days
{
    public class Day07 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var fileSystem = GetFileSystem(input);

            SetDirectorySizes(fileSystem.First());

            long freeSpace = 70000000 - fileSystem.First().FileSize;
            long minNeeded = 30000000 - freeSpace;
            long smallestDirectoryToDelete = long.MaxValue;
            foreach (var dir in fileSystem.Skip(1))
            {
                if (dir.FileSize < smallestDirectoryToDelete && dir.FileSize > minNeeded)
                {
                    smallestDirectoryToDelete = dir.FileSize;
                }
            }

            var resPartOne = fileSystem.Where(x => x.FileSize <= 100000).Sum(x => (long)x.FileSize);
            var resPartTwo = smallestDirectoryToDelete;

            return (day, resPartOne.ToString(), resPartTwo.ToString());
        }


        private static void SetDirectorySizes(Directory root)
        {
            foreach(var dir in root.Children)
            {
                SetDirectorySizes(dir);
                var total = dir.FileSize;
                if(dir.Parent != null)
                {
                    dir.Parent.AddFileSize(total);
                }
            }
        }

        private static List<Directory> GetFileSystem(IEnumerable<string> input)
        {
            var fileSystem = new List<Directory>() { new Directory("/", null) };
            var current = fileSystem.First();
            var currentPath = new Stack<string>();
            currentPath.Push("/");

            foreach (var line in input.ToList().Skip(1))
            {
                if (line.StartsWith("$ ls") || line.StartsWith("dir"))
                {
                    continue;
                }
                else if (line.StartsWith("$ cd .."))
                {
                    currentPath.Pop();
                    current = current?.Parent;
                }
                else if (line.StartsWith("$ cd"))
                {
                    var directoryName = line.Split(" ")[2];
                    var newPath = currentPath.Peek() + directoryName;
                    currentPath.Push(newPath);

                    var parent = current;
                    current = fileSystem.FirstOrDefault(x => x.Name.Equals(newPath));
                    
                    if (current == null)
                    {
                        current = new Directory(newPath, parent);
                        fileSystem.Add(current);
                        parent?.AddChild(current);
                    }
                }
                else
                {
                    var file = int.Parse(line.Split(" ")[0]);
                    current?.AddFileSize(file);
                }
            }

            return fileSystem;
        }
    }

    public class Directory
    {
        private List<Directory> children = new List<Directory>();
        public Directory(string name, Directory? parent)
        {
            Name = name;
            Parent = parent;
        }

        public List<Directory>Children => children;
        public string Name { get; set; }
        public long FileSize { get; private set; }
        public Directory? Parent { get; private set; }
        public void AddChild(Directory dir)
        {
            children.Add(dir);
        }

        public void AddFileSize(long size)
        {
            FileSize += size;
        }
    }
}
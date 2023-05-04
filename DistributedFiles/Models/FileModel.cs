using System;

namespace DistributedFiles.Models
{
  public class FileModel
  {
    public string Name { get; set; }
    public string Path { get; set; }
    public DateTime ModifyDate { get; set; }
  }
}
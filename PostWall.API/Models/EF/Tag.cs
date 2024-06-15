﻿namespace PostWall.API.Models.EF;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Post>? Posts { get; set; }
}
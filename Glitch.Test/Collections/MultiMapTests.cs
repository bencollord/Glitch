using FluentAssertions;
using Glitch.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Test.Collections;

#pragma warning disable IDE0028 // Collection initialiation can b e simplified (when using collection.Add)
public class MultiMapTests
{
    [Fact]
    public void AfterConstruction_KeyCountAndEntryCount_ShouldReturnCorrectValues()
    {
        // Arrange/Act
        var map = new MultiMap<string, int>()
        {
            ["Foo"] = [1, 2, 3],
            ["Bar"] = [4, 5, 6],
            ["Baz"] = [7, 8, 9],
        };

        // Assert
        map.KeyCount.Should().Be(3);
        map.EntryCount.Should().Be(9);
    }

    [Fact]
    public void Enumerator_ShouldEnumerateAllItems()
    {
        // Arrange
        var map = new MultiMap<string, int>()
        {
            ["Foo"] = [1, 2, 3],
            ["Bar"] = [4, 5, 6],
            ["Baz"] = [7, 8, 9],
        };

        // Act
        var entries = new List<KeyValuePair<string, int>>();

        using var iter = map.GetEnumerator();

        while (iter.MoveNext())
        {
            entries.Add(iter.Current);
        }

        // Assert
        entries.Count.Should().Be(9);

        entries[0..2].Should().AllSatisfy(e => e.Key.Should().Be("Foo"));
        entries[3..5].Should().AllSatisfy(e => e.Key.Should().Be("Bar"));
        entries[6..8].Should().AllSatisfy(e => e.Key.Should().Be("Baz"));

        entries[0].Value.Should().Be(1);
        entries[1].Value.Should().Be(2);
        entries[2].Value.Should().Be(3);
        entries[3].Value.Should().Be(4);
        entries[4].Value.Should().Be(5);
        entries[5].Value.Should().Be(6);
        entries[6].Value.Should().Be(7);
        entries[7].Value.Should().Be(8);
        entries[8].Value.Should().Be(9);
    }

    [Fact]
    public void Add_KeyDoesNotExist_ShouldAddNewKey()
    {
        // Arrange
        var map = new MultiMap<string, int>();

        // Act
        map.Add("Foo", 22);

        // Assert
        map.ContainsKey("Foo").Should().BeTrue();
        map["Foo"].Should().HaveCount(1)
                  .And.BeEquivalentTo([22]);
    }

    [Fact]
    public void Add_KeyExists_ShouldAppendToList()
    {
        // Arrange
        var map = new MultiMap<string, int>()
        {
            ["Foo"] = [22, 33, 44]
        };

        // Act
        map.Add("Foo", 55);

        // Assert
        map["Foo"].Should().HaveCount(4)
                  .And.BeEquivalentTo([22, 33, 44, 55]);
    }

    [Fact]
    public void AddRange_KeyDoesNotExist_ShouldAddNewKey()
    {
        // Arrange
        var map = new MultiMap<string, int>();

        // Act
        map.AddRange("Foo", [1, 2, 3, 4]);

        // Assert
        map.ContainsKey("Foo").Should().BeTrue();
        map["Foo"].Should().HaveCount(4)
                  .And.BeEquivalentTo([1, 2, 3, 4]);
    }

    [Fact]
    public void AddRange_KeyExists_ShouldAppendToList()
    {
        // Arrange
        var map = new MultiMap<string, int>()
        {
            ["Foo"] = [1, 2, 3, 4]
        };

        // Act
        map.AddRange("Foo", [5, 6, 7, 8]);

        // Assert
        map["Foo"].Should().HaveCount(8)
                  .And.BeEquivalentTo([1, 2, 3, 4, 5, 6, 7, 8]);
    }

    [Fact]
    public void RemoveAt_EntryExists_ShouldRemoveEntryAtIndexAndReturnTrue()
    {
        // Arrange
        var map = new MultiMap<string, int>()
        {
            ["Foo"] = [1, 2, 3],
            ["Bar"] = [4, 5, 6],
            ["Baz"] = [7, 8, 9],
        };

        // Act/Assert
        map.RemoveAt("Foo", 1).Should().BeTrue();

        map["Foo"].Should().HaveCount(2)
                  .And.BeEquivalentTo([1, 3]);
    }

    [Fact]
    public void Remove_EntryExists_ShouldRemoveEntryAndReturnTrue()
    {
        // Arrange
        var map = new MultiMap<string, int>()
        {
            ["Foo"] = [1, 2, 3],
            ["Bar"] = [4, 5, 6],
            ["Baz"] = [7, 8, 9],
        };

        // Act/Assert
        map.Remove("Foo", 2).Should().BeTrue();

        map["Foo"].Should().HaveCount(2)
                  .And.BeEquivalentTo([1, 3]);
    }

    [Fact]
    public void Remove_LastEntryRemovedForKey_DeletesKey()
    {
        // Arrange
        var map = new MultiMap<string, int>()
        {
            ["Foo"] = [1, 2, 3],
            ["Bar"] = [4, 5, 6],
            ["Baz"] = [7, 8, 9],
        };

        // Act/Assert
        map.Remove("Foo", 1).Should().BeTrue();
        map.ContainsKey("Foo").Should().BeTrue();

        map.Remove("Foo", 2).Should().BeTrue();
        map.ContainsKey("Foo").Should().BeTrue();

        map.Remove("Foo", 3).Should().BeTrue();
        map.ContainsKey("Foo").Should().BeFalse();
    }

    [Fact]
    public void RemoveAll_ShouldRemoveKeyEntirely_AndReturnRemovedCount()
    {
        // Arrange
        var map = new MultiMap<string, int>()
        {
            ["Foo"] = [1, 2, 3],
            ["Bar"] = [4, 5, 6],
            ["Baz"] = [7, 8, 9],
        };

        // Act
        int removed = map.RemoveAll("Foo");

        // Assert
        map.ContainsKey("Foo").Should().BeFalse();
        removed.Should().Be(3);
    }
}

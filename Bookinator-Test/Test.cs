using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Test
{
	public class Test
	{
		private int id;

		public Test()
		{
			this.id = 1;
		}

		protected int GetID()
		{
			return id++;
		}

		public void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) where T : class
		{
			var expectedList = expected.ToList();
			var actualList = actual.ToList();
			CollectionAssert.AreEqual(expectedList, actualList);
		}

		public void AreEqual(IEnumerable<int> expected, IEnumerable<int> actual)
		{
			var expectedList = expected.ToList();
			var actualList = actual.ToList();
			CollectionAssert.AreEqual(expectedList, actualList);
		}

		public void HaveSameProps<T>(IEnumerable<T> expected, IEnumerable<T> actual) where T : class
		{
			var expectedList = expected.ToList();
			var actualList = actual.ToList();

			var expectedCount = expectedList.Count;
			Assert.AreEqual(expectedCount, actualList.Count, "Different number of elements");

			for (var i = 0; i < expectedCount; i++)
			{
				var item1 = expectedList.ElementAt(i);
				var item2 = actualList.ElementAt(i);
				HaveSameProps<T>(item1, item2);
			}
		}

		public void HaveSameProps<T>(T item1, T item2) where T : class
		{
			var type = item1.GetType();
			var props = type.GetProperties();
			foreach (var prop in props)
			{
				var name = prop.Name;
				var item1Value = prop.GetValue(item1);
				var item2Value = prop.GetValue(item2);
				var failMessage = "Property: " + name;
				Assert.AreEqual(item1Value, item2Value, failMessage);
			}
		}

		public void IsEmpty<T>(IEnumerable<T> actual) where T : class
		{
			Assert.IsFalse(actual.Any(), "Expected empty set but set contains elements");
		}

		public void IsEmpty(IEnumerable<int> actual)
		{
			Assert.IsFalse(actual.Any(), "Expected empty set but set contains elements");
		}

		protected IEnumerable<T> GetList<T>() where T : class
		{
			return new List<T>();
		}

		protected IEnumerable<T> GetList<T>(params T[] entities) where T : class
		{
			return entities.AsEnumerable();
		}

		protected IEnumerable<int> GetList(params int[] entities)
		{
			return entities.AsEnumerable();
		}
	}
}

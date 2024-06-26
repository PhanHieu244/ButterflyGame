﻿using System;
using DG.Tweening;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scroller
{
	using System.Linq;

	public interface IScrollView
	{
		void LoadData(SmallList<ICellData> data);
		int GetCurrentCellViewIndexAtPosition();
		public void ScrollToItem(int index, float duration = 0.2f,
			EnhancedScroller.TweenType tweenType = EnhancedScroller.TweenType.easeInCirc, Action completeAction = null);
	}
	
	public class AdvanceScrollView : SerializedMonoBehaviour, IEnhancedScrollerDelegate, IScrollView
	{
		/// <summary>
		/// Internal representation of our data. Note that the scroller will never see
		/// this, so it separates the data from the layout using MVC principles.
		/// </summary>
		private SmallList<ICellData> _data = new ();

		/// <summary>
		/// This is our scroller we will be a delegate for
		/// </summary>
		public EnhancedScroller scroller;

		/// <summary>
		/// This will be the prefab of each cell in our scroller. The cell view will
		/// hold references to each row sub cell
		/// </summary>
		public ICellView[] cellViewPrefabs;

		public int numberOfCellsPerRow = 1;

		/// <summary>
		/// Be sure to set up your references to the scroller after the Awake function. The 
		/// scroller does some internal configuration in its own Awake function. If you need to
		/// do this in the Awake function, you can set up the script order through the Unity editor.
		/// In this case, be sure to set the EnhancedScroller's script before your delegate.
		/// 
		/// In this example, we are calling our initializations in the delegate's Start function,
		/// but it could have been done later, perhaps in the Update function.
		/// </summary>
		protected virtual void Start()
		{
			// tell the scroller that this script will be its delegate
			scroller.Delegate = this;
		}

		/// <summary>
		/// Populates the data with a lot of records
		/// </summary>
		public void LoadData(SmallList<ICellData> data)
		{
			_data = data;

			// tell the scroller to reload now that we have the data
			scroller.ReloadData();
			//scroller.SetToTop();
			//ScrollToTop();
		}

		public int GetCurrentCellViewIndexAtPosition()
		{
			return scroller.GetCurrentCellViewIndexAtPosition();
		}

		public void UpdateData(SmallList<ICellData> data) { _data = data; }

		public void ScrollToTop()
		{
			// ReSharper disable once Unity.NoNullPropagation
			scroller.ScrollRect?.DONormalizedPos(Vector2.one, 0);
		}

		#region EnhancedScroller Handlers

		/// <summary>
		/// This tells the scroller the number of cells that should have room allocated.
		/// For this example, the count is the number of data elements divided by the number of cells per row (rounded up using Mathf.CeilToInt)
		/// </summary>
		/// <param name="scroller">The scroller that is requesting the data size</param>
		/// <returns>The number of cells</returns>
		public int GetNumberOfCells(EnhancedScroller scroller)
		{
			return Mathf.CeilToInt((float) _data.Count / (float) numberOfCellsPerRow);
		}

		/// <summary>
		/// This tells the scroller what the size of a given cell will be. Cells can be any size and do not have
		/// to be uniform. For vertical scrollers the cell size will be the height. For horizontal scrollers the
		/// cell size will be the width.
		/// </summary>
		/// <param name="scroller">The scroller requesting the cell size</param>
		/// <param name="dataIndex">The index of the data that the scroller is requesting</param>
		/// <returns>The size of the cell</returns>
		public virtual float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
		{
			return GetCellSize(_data[dataIndex]);
		}

		private float GetCellSize(ICellData data)
		{
			var cellView = GetCellPrefab(data);

			return cellView.CellViewSize(data);
		}

		private ICellView GetCellPrefab(ICellData data)
		{
			return cellViewPrefabs.ToList().Find(match => match.CellIdentifier == data.Identifier);
		}

		/// <summary>
		/// Gets the cell to be displayed. You can have numerous cell types, allowing variety in your list.
		/// Some examples of this would be headers, footers, and other grouping cells.
		/// </summary>
		/// <param name="scroller">The scroller requesting the cell</param>
		/// <param name="dataIndex">The index of the data that the scroller is requesting</param>
		/// <param name="cellIndex">The index of the list. This will likely be different from the dataIndex if the scroller is looping</param>
		/// <returns>The cell for the scroller to use</returns>
		public virtual ICellView GetCellView(EnhancedScroller scroller,
			int dataIndex, int cellIndex)
		{
			var cellPrefab = GetCellPrefab(_data[dataIndex]);
			// first, we get a cell from the scroller by passing a prefab.
			// if the scroller finds one it can recycle it will do so, otherwise
			// it will create a new cell.
			ICellView cellItemView = scroller.GetCellView(cellPrefab);

			// pass in a reference to our data set with the offset for this cell
			cellItemView?.SetData(ref _data, dataIndex * numberOfCellsPerRow);

			// return the cell to the scroller
			return cellItemView;
		}

		#endregion

		public void MoveScrollViewToItem(int index)
		{
			index = Mathf.Clamp(index, 0, _data.Count);
			scroller.JumpToDataIndex(index);
		}

		public void ScrollToItem(int index, float duration = 0.2f, EnhancedScroller.TweenType tweenType = EnhancedScroller.TweenType.easeInCirc, Action completeAction = null)
		{
			index = Mathf.Clamp(index, 0, _data.Count);
			scroller.JumpToDataIndex(index, tweenType: tweenType,
				tweenTime: duration, jumpComplete: completeAction);
		}

		public SmallList<ICellData> Data => _data;
	}
}
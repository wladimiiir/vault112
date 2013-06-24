package fonline.model

import swing.Publisher
import swing.event.Event

/**
 * User: mikewall
 * Date: 10/27/12
 * Time: 5:04 PM
 */
class ItemModel[A](private var _items: List[A]) extends Publisher {
  private var _selectedItem: Option[A] = None

  def selectedItem = _selectedItem

  def selectedItem_=(item: Option[A]) {
    if (_selectedItem != item) {
      _selectedItem = item
      publish(ItemSelectionChanged[A](item))
    }
  }

  def items = _items

  def items_=(items: List[A]) {
    val oldItems = _items
    _items = items
    publish(ItemsChanged[A](oldItems, items))
    selectedItem = None
  }

  def addItem(item: A) {
    _items :+= item
    publish(ItemAdded[A](item))
  }

  def removeItem(item: A) {
    _items = items.filterNot(_ == item)
    publish(ItemRemoved[A](item))
  }

  def find(f: (A => Boolean)) = items.find(f)

  def fireItemChanged(item: A) {
    publish(ItemChanged[A](item))
  }

  def size = items.size
}

case class ItemSelectionChanged[A](item: Option[A]) extends Event

case class ItemAdded[A](item: A) extends Event

case class ItemRemoved[A](item: A) extends Event

case class ItemChanged[A](item: A) extends Event

case class ItemsChanged[A](oldItems: List[A], newItems: List[A]) extends Event


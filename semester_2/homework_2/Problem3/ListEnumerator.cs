namespace ListStuff
{
    using System.Collections;

    internal class ListEnumerator<T> : IEnumerator
    {
        private List<T>.ListElement listBegin;
        private List<T>.ListElement currentPosition;

        public ListEnumerator(List<T>.ListElement list)
        {
            this.listBegin = list;
            this.currentPosition = null;
        }

        public object Current
        {
            get
            {
                return this.currentPosition.Value;
            }

            set
            {
                this.currentPosition.Value = (T)value;
            }
        }

        public bool MoveNext()
        {
            if (this.listBegin == null ||
                (this.currentPosition != null && this.currentPosition.Next == null))
            {
                return false;
            }
            else if (this.currentPosition == null)
            {
                this.currentPosition = this.listBegin;
            }
            else
            {
                this.currentPosition = this.currentPosition.Next;
            }

            return true;
        }

        public void Reset()
        {
            this.currentPosition = null;
        }
    }
}
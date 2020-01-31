namespace DragSystem
{
    public interface IGgDrag
    {
        void OnGgDrag();
    }
    
    public interface IGgPointerDown
    {
        void OnGgPointerDown();
    }
    
    public interface IGgPointerUp
    {
        void OnGgPointerUp();
    }
    
    public interface IGgPointerEnter
    {
        void OnGgPointerEnter();
    }
    
    public interface IGgPointerExit
    {
        void OnGgPointerExit();
    }
}
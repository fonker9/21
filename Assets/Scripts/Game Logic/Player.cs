using Fusion;

public class Player : NetworkBehaviour
{

    private void Awake()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
        }
    }
    
}
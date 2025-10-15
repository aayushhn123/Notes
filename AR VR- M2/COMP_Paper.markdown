# Introduction to AR VR - Test-I Solutions

**Date: 7-10-2026, Time: 6:00 PM - 7:00 PM**  
**Marks: 10**

## Instructions
- Figures in brackets on the right-hand side indicate full marks.
- Assume suitable data if necessary.
- Question 1 is compulsory.
- Answer any 2 from the remaining questions.

## Set A Solutions

### Q1a: Player Cube Moving with WASD
**CO-4, SO-2, B.L.-3, Marks: [1*2]**

**Steps:**
1. Create a new 3D project named "ARVRTest_SetA_Q1a" in Unity Hub.
2. In the Hierarchy, right-click > 3D Object > Plane, name it "Ground", set Position to (0, 0, 0), Scale to (10, 1, 10), and add a Box Collider.
3. Right-click in Hierarchy > 3D Object > Cube, name it "Player", set Position to (0, 0.5, 0), and add a Rigidbody and Box Collider.
4. In the Project window, create a folder named "Scripts". Right-click > Create > C# Script, name it "PlayerMovement".
5. Open "PlayerMovement.cs", replace its content with the code below, and save.
6. Drag the "PlayerMovement" script onto the "Player" GameObject in the Hierarchy.
7. In the Inspector, ensure the "Player" GameObject has the "Player" tag (add it via Tag dropdown > Add Tag > Create "Player" if missing).
8. Press Play in the Unity Editor, test WASD keys for movement (W: forward, A: left, S: backward, D: right), and ESC to stop in Editor or quit in build.
9. To test the build, go to File > Build Settings, add the current scene, and build the project.

**Code:**
```csharp
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement, adjustable in Inspector

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component for physics
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // A/D for left/right (-1 for left, 1 for right)
        float verticalInput = Input.GetAxisRaw("Vertical"); // W/S for forward/backward (-1 for back, 1 for forward)
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime; // Frame-rate independent
        transform.Translate(movement, Space.Self); // Move in all directions relative to cube's orientation

        if (Input.GetKeyDown(KeyCode.Escape)) // Check if ESC is pressed
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Editor
#else
            Application.Quit(); // Quit the application in build
#endif
        }
    }
}
```

**Submission Requirements:**
- Script screenshot showing WASD movement and ESC logic.
- Hierarchy screenshot showing "Player" and "Ground" with proper naming.
- Game window screenshot showing cube movement with WASD.

### Q1b: TextMeshPro Element Displaying "Ready to Move"
**CO-4, SO-3, B.L.-3, Marks: [1*2]**

**Steps:**
1. In the Hierarchy, right-click > UI > Canvas, name it "UICanvas".
2. With "UICanvas" selected, right-click > UI > Text - TextMeshPro, name it "MovementText", set its Rect Transform Position to (0, 200, 0) in the Canvas (adjust as needed to appear above Player).
3. In the Inspector, set "MovementText" Text to "Ready to Move", Font Size to 24, and Alignment to Center.
4. Ensure TextMeshPro is imported (Window > TextMeshPro > Import TMP Essential Resources if not done).
5. In the Project window, create a "Scripts" folder if not exists, right-click > Create > C# Script, name it "TextDisplay".
6. Open "TextDisplay.cs", replace its content with the code below, and save.
7. Drag the "TextDisplay" script onto the "UICanvas" GameObject.
8. In the Inspector, drag the "MovementText" GameObject to the "Text Display" field of the "TextDisplay" component.
9. Press Play, ensure "Ready to Move" appears above the Player.

**Code:**
```csharp
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; // Assign MovementText in Inspector

    void Start()
    {
        if (textDisplay != null) // Ensure text object is assigned
        {
            textDisplay.text = "Ready to Move"; // Set initial text
        }
    }
}
```

**Submission Requirements:**
- Hierarchy screenshot showing "MovementText" under "UICanvas" and linked to Player.
- Game window screenshot showing "Ready to Move" text visible.
- Inspector screenshot showing "MovementText" properties.

### Q2: Obstacle Disappears on Collision
**CO-3, SO-6, B.L.-5, Marks: [3]**

**Steps:**
1. In the Hierarchy, right-click > 3D Object > Cube, name it "Obstacle", set Position to (2, 0.5, 0), and add a Box Collider.
2. Add a Rigidbody to "Obstacle", uncheck "Use Gravity" to keep it static.
3. In the Hierarchy, ensure "UICanvas" exists (from Q1b), right-click under it > UI > Text - TextMeshPro, name it "StatusText", set Rect Transform Position to (0, 400, 0) (top-center), and set Text to empty, Font Size to 24, Alignment to Center.
4. In the Project window, in "Scripts" folder, right-click > Create > C# Script, name it "ObstacleCollision".
5. Open "ObstacleCollision.cs", replace its content with the code below, and save.
6. Drag the "ObstacleCollision" script onto the "Obstacle" GameObject.
7. In the Inspector, drag the "StatusText" GameObject to the "Status Text" field of the "ObstacleCollision" component.
8. Ensure "Player" has the "Player" tag (from Q1a).
9. Press Play, move "Player" to collide with "Obstacle", verify it disappears and "Obstacle Cleared!" appears.

**Code:**
```csharp
using UnityEngine;
using TMPro;

public class ObstacleCollision : MonoBehaviour
{
    public TextMeshProUGUI statusText; // Assign StatusText in Inspector

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if Player collides
        {
            gameObject.SetActive(false); // Disable the obstacle
            if (statusText != null)
            {
                statusText.text = "Obstacle Cleared!"; // Update TMP text
            }
        }
    }
}
```

**Submission Requirements:**
- Script screenshot showing collision logic.
- Inspector screenshot of "Obstacle" with Rigidbody and script.
- Game window screenshots before and after collision.
- Hierarchy screenshot showing "Obstacle" and "StatusText".

### Q3: Trigger Zone with Falling Cube
**CO-3, SO-1, B.L.-4, Marks: [3]**

**Steps:**
1. In the Hierarchy, right-click > Create Empty, name it "TriggerZone", set Position to (0, 0, 5).
2. Under "TriggerZone", right-click > 3D Object > Cube, name it "TriggerArea", set Scale to (2, 1, 2), add a Box Collider, and check "Is Trigger".
3. Right-click in Hierarchy > 3D Object > Cube, name it "FallingCube", set Position to (0, 5, 5), and add a Rigidbody (uncheck "Use Gravity" initially).
4. Ensure "UICanvas" exists, right-click under it > UI > Text - TextMeshPro, name it "GravityText", set Rect Transform Position to (0, 400, 0), and set Text to empty, Font Size to 24, Alignment to Center.
5. In the "Scripts" folder, right-click > Create > C# Script, name it "TriggerGravity".
6. Open "TriggerGravity.cs", replace its content with the code below, and save.
7. Drag the "TriggerGravity" script onto the "TriggerArea" GameObject (not "TriggerZone").
8. In the Inspector, drag "FallingCube" to the "Falling Cube" field and "GravityText" to the "Gravity Text" field of the "TriggerGravity" component.
9. Ensure "Player" has the "Player" tag.
10. Press Play, move "Player" into "TriggerArea", verify "FallingCube" falls and "Gravity Enabled" appears.

**Code:**
```csharp
using UnityEngine;
using TMPro;

public class TriggerGravity : MonoBehaviour
{
    public GameObject fallingCube; // Assign FallingCube in Inspector
    public TextMeshProUGUI gravityText; // Assign GravityText in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if Player enters
        {
            if (fallingCube != null)
            {
                Rigidbody rb = fallingCube.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true; // Enable gravity to make it fall
                }
            }
            if (gravityText != null)
            {
                gravityText.text = "Gravity Enabled"; // Display message
            }
        }
    }
}
```

**Submission Requirements:**
- Script screenshot showing trigger logic.
- Hierarchy screenshot showing "TriggerZone" with "TriggerArea" and "FallingCube".
- Game window screenshot showing "FallingCube" falling and "GravityText".

### Q4: Rotating Obstacle that Stops on Collision
**CO-4, SO-2, B.L.-6, Marks: [3]**

**Steps:**
1. In the Hierarchy, right-click > 3D Object > Cube, name it "RotatingObstacle", set Position to (2, 0.5, 2), and add a Box Collider.
2. Add a Rigidbody to "RotatingObstacle", uncheck "Use Gravity".
3. Ensure "UICanvas" exists, right-click under it > UI > Text - TextMeshPro, name it "StopText", set Rect Transform Position to (0, 400, 0), and set Text to empty, Font Size to 24, Alignment to Center.
4. In the "Scripts" folder, right-click > Create > C# Script, name it "RotatingObstacle".
5. Open "RotatingObstacle.cs", replace its content with the code below, and save.
6. Drag the "RotatingObstacle" script onto the "RotatingObstacle" GameObject.
7. In the Inspector, drag the "StopText" GameObject to the "Stop Text" field of the "RotatingObstacle" component.
8. Ensure "Player" has the "Player" tag.
9. Press Play, verify "RotatingObstacle" rotates, stops on collision with "Player", and "Stopped" appears.

**Code:**
```csharp
using UnityEngine;
using TMPro;

public class RotatingObstacle : MonoBehaviour
{
    public float rotationSpeed = 50f; // Rotation speed in degrees per second
    public TextMeshProUGUI stopText; // Assign StopText in Inspector
    private bool isRotating = true; // Flag to control rotation

    void Update()
    {
        if (isRotating) // Rotate only if flag is true
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World); // Rotate around Y-axis
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check for Player collision
        {
            isRotating = false; // Stop rotation
            if (stopText != null)
            {
                stopText.text = "Stopped"; // Update TMP text
            }
        }
    }
}
```

**Submission Requirements:**
- Script screenshot showing rotation and stop logic.
- Inspector screenshot of "RotatingObstacle" with Rigidbody and script.
- Game window screenshots before and after collision.

## Test-I SET-B Solutions

### Q1a: Player Cube Moving Left and Right
**CO-4, SO-6, B.L.-3, Marks: [1*2]**

**Steps:**
1. Create a new 3D project named "ARVRTest_SetB_Q1a" in Unity Hub.
2. In the Hierarchy, right-click > 3D Object > Plane, name it "Ground", set Scale to (10, 1, 10), and add a Box Collider.
3. Right-click > 3D Object > Cube, name it "Player", set Position to (0, 0.5, 0), and add a Rigidbody and Box Collider.
4. In the Project window, create a "Scripts" folder, right-click > Create > C# Script, name it "PlayerSideMovement".
5. Open "PlayerSideMovement.cs", replace its content with the code below, and save.
6. Drag the "PlayerSideMovement" script onto the "Player" GameObject.
7. In the Inspector, add the "Player" tag to the "Player" GameObject if missing.
8. Press Play, test A/D keys for left/right movement, and ESC to stop in Editor or quit in build.
9. Build via File > Build Settings > Add Open Scenes > Build.

**Code:**
```csharp
using UnityEngine;

public class PlayerSideMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of side-to-side movement

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody for physics
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // A/D for left/right
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime; // Frame-rate independent
        transform.Translate(movement, Space.Self); // Move left/right

        if (Input.GetKeyDown(KeyCode.Escape)) // ESC handling
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop in Editor
#else
            Application.Quit(); // Quit in build
#endif
        }
    }
}
```

**Submission Requirements:**
- Script screenshot showing A/D movement and ESC logic.
- Game window screenshot showing cube movement.
- Hierarchy screenshot showing "Player" and "Ground".

### Q1b: Display Player’s Position
**CO-4, SO-2, B.L.-3, Marks: [1*2]**

**Steps:**
1. In the Hierarchy, right-click > UI > Canvas, name it "UICanvas".
2. With "UICanvas" selected, right-click > UI > Text - TextMeshPro, name it "PositionText", set Rect Transform Position to (0, 200, 0) above Player.
3. In the Inspector, set "PositionText" Text to empty, Font Size to 24, Alignment to Center.
4. Ensure TextMeshPro is imported (Window > TextMeshPro > Import TMP Essential Resources if needed).
5. In the "Scripts" folder, right-click > Create > C# Script, name it "PositionDisplay".
6. Open "PositionDisplay.cs", replace its content with the code below, and save.
7. Drag the "PositionDisplay" script onto the "UICanvas" GameObject.
8. In the Inspector, drag the "Player" GameObject to the "Player" field and "PositionText" to the "Position Text" field of the "PositionDisplay" component.
9. Press Play, move "Player" with A/D, verify position updates in real-time.

**Code:**
```csharp
using UnityEngine;
using TMPro;

public class PositionDisplay : MonoBehaviour
{
    public TextMeshProUGUI positionText; // Assign PositionText in Inspector
    public Transform player; // Assign Player in Inspector

    void Update()
    {
        if (positionText != null && player != null)
        {
            Vector3 pos = player.position; // Get player’s position
            positionText.text = $"Position: ({pos.x:F1}, {pos.y:F1}, {pos.z:F1})"; // Display with 1 decimal
        }
    }
}
```

**Submission Requirements:**
- Hierarchy screenshot showing "PositionText" under "UICanvas".
- Game window screenshot showing position text updating.
- Inspector screenshot showing "PositionDisplay" component.

### Q2: Obstacle Changes Color on Collision
**CO-3, SO-6, B.L.-5, Marks: [3]**

**Steps:**
1. In the Hierarchy, right-click > 3D Object > Cube, name it "ColorObstacle", set Position to (2, 0.5, 0), and add a Box Collider.
2. Ensure "UICanvas" exists, right-click under it > UI > Text - TextMeshPro, name it "CollisionText", set Rect Transform Position to (0, 400, 0), and set Text to empty, Font Size to 24, Alignment to Center.
3. In the "Scripts" folder, right-click > Create > C# Script, name it "ColorChangeCollision".
4. Open "ColorChangeCollision.cs", replace its content with the code below, and save.
5. Drag the "ColorChangeCollision" script onto the "ColorObstacle" GameObject.
6. In the Inspector, drag "CollisionText" to the "Collision Text" field of the "ColorChangeCollision" component.
7. Ensure "Player" has the "Player" tag.
8. Press Play, move "Player" to collide with "ColorObstacle", verify it turns red and "Collision Detected!" appears.

**Code:**
```csharp
using UnityEngine;
using TMPro;

public class ColorChangeCollision : MonoBehaviour
{
    public TextMeshProUGUI collisionText; // Assign CollisionText in Inspector
    public Color newColor = Color.red; // Color to change to

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Renderer>().material.color = newColor; // Change obstacle color
            if (collisionText != null)
            {
                collisionText.text = "Collision Detected!"; // Update TMP
            }
        }
    }
}
```

**Submission Requirements:**
- Script screenshot showing collision logic.
- Inspector screenshot of "ColorObstacle" material and script.
- Game window screenshots before and after collision.
- Hierarchy screenshot showing "ColorObstacle" and "CollisionText".

### Q3: Trigger Object Rotates Object on Collision
**CO-3, SO-2, B.L.-5, Marks: [3]**

**Steps:**
1. In the Hierarchy, right-click > Create Empty, name it "RotationTrigger", set Position to (0, 0, 5).
2. Under "RotationTrigger", right-click > 3D Object > Cube, name it "TriggerArea", set Scale to (2, 1, 2), add a Box Collider, and check "Is Trigger".
3. Right-click in Hierarchy > 3D Object > Cube, name it "RotatableObject", set Position to (0, 0.5, 5).
4. Ensure "UICanvas" exists, right-click under it > UI > Text - TextMeshPro, name it "ZoneText", set Rect Transform Position to (0, 400, 0), and set Text to empty, Font Size to 24, Alignment to Center.
5. In the "Scripts" folder, right-click > Create > C# Script, name it "RotationTrigger".
6. Open "RotationTrigger.cs", replace its content with the code below, and save.
7. Drag the "RotationTrigger" script onto the "TriggerArea" GameObject.
8. In the Inspector, drag "RotatableObject" to the "Rotatable Object" field and "ZoneText" to the "Zone Text" field.
9. Ensure "Player" has the "Player" tag.
10. Press Play, move "Player" into "TriggerArea", verify "RotatableObject" rotates and "Active Zone" appears while inside.

**Code:**
```csharp
using UnityEngine;
using TMPro;

public class RotationTrigger : MonoBehaviour
{
    public GameObject rotatableObject; // Assign RotatableObject in Inspector
    public TextMeshProUGUI zoneText; // Assign ZoneText in Inspector
    public float rotationSpeed = 50f; // Speed of rotation
    private bool isActive = false; // Track if player is inside

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true; // Start rotation
            if (zoneText != null)
            {
                zoneText.text = "Active Zone"; // Display text
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false; // Stop rotation
            if (zoneText != null)
            {
                zoneText.text = ""; // Clear text
            }
        }
    }

    void Update()
    {
        if (isActive && rotatableObject != null)
        {
            rotatableObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World); // Rotate while active
        }
    }
}
```

**Submission Requirements:**
- Script screenshot showing trigger and rotation logic.
- Hierarchy screenshot showing "RotationTrigger" with "TriggerArea" and "RotatableObject".
- Game window screenshot with "ZoneText" visible.

### Q4: Trigger Changes Color on Collision
**CO-4, SO-2, B.L.-6, Marks: [3]**

**Steps:**
1. In the Hierarchy, right-click > Create Empty, name it "ColorTrigger", set Position to (0, 0, 5).
2. Under "ColorTrigger", right-click > 3D Object > Cube, name it "TriggerArea", set Scale to (2, 1, 2), add a Box Collider, and check "Is Trigger".
3. Right-click in Hierarchy > 3D Object > Cube, name it "ColorObject", set Position to (0, 0.5, 5).
4. Ensure "UICanvas" exists, right-click under it > UI > Text - TextMeshPro, name it "ZoneText", set Rect Transform Position to (0, 400, 0), and set Text to empty, Font Size to 24, Alignment to Center.
5. In the "Scripts" folder, right-click > Create > C# Script, name it "ColorTrigger".
6. Open "ColorTrigger.cs", replace its content with the code below, and save.
7. Drag the "ColorTrigger" script onto the "TriggerArea" GameObject.
8. In the Inspector, drag "ColorObject" to the "Color Object" field and "ZoneText" to the "Zone Text" field.
9. Ensure "Player" has the "Player" tag.
10. Press Play, move "Player" into "TriggerArea", verify "ColorObject" turns blue while inside and reverts when exiting.

**Code:**
```csharp
using UnityEngine;
using TMPro;

public class ColorTrigger : MonoBehaviour
{
    public GameObject colorObject; // Assign ColorObject in Inspector
    public TextMeshProUGUI zoneText; // Assign ZoneText in Inspector
    public Color activeColor = Color.blue; // Color when active
    private Color originalColor; // Store original color
    private bool isActive = false; // Track if player is inside

    void Start()
    {
        if (colorObject != null)
        {
            originalColor = colorObject.GetComponent<Renderer>().material.color; // Save original color
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true; // Activate color change
            if (colorObject != null)
            {
                colorObject.GetComponent<Renderer>().material.color = activeColor; // Change to active color
            }
            if (zoneText != null)
            {
                zoneText.text = "Active Zone"; // Display text
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false; // Deactivate
            if (colorObject != null)
            {
                colorObject.GetComponent<Renderer>().material.color = originalColor; // Restore original color
            }
            if (zoneText != null)
            {
                zoneText.text = ""; // Clear text
            }
        }
    }
}
```

**Submission Requirements:**
- Script screenshot showing trigger and color logic.
- Hierarchy screenshot showing "ColorTrigger" with "TriggerArea" and "ColorObject".
- Game window screenshots before and after entering trigger.

## Notes
- Ensure the "Player" tag is added to the "Player" GameObject in the Inspector (Tag dropdown > Add Tag > Create "Player").
- Import TextMeshPro via Window > TextMeshPro > Import TMP Essential Resources if not already done.
- Build the project via File > Build Settings > Add Open Scenes > Build for ESC to work in .exe.
- Use Unity 2021.3 LTS or newer for compatibility.

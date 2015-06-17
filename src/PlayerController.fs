namespace Space_Shooter

open UnityEngine

type Boundary() =
    class
        [<SerializeField>] [<DefaultValue>] val mutable xmin: float32
        [<SerializeField>] [<DefaultValue>] val mutable xmax: float32
        [<SerializeField>] [<DefaultValue>] val mutable zmin: float32
        [<SerializeField>] [<DefaultValue>] val mutable zmax: float32
    end

type PlayerController() = 
    inherit MonoBehaviour()

    [<SerializeField>] 
    let mutable speedScale: float32 = 1.0f
    [<SerializeField>] 
    let mutable tiltFactor: float32 = 1.0f
    [<SerializeField>] 
    let mutable fireRate: float32 = 1.0f

    [<SerializeField>] [<DefaultValue>] val mutable boundary: Boundary
    [<SerializeField>] [<DefaultValue>] val mutable shotSpawn: Transform
    [<SerializeField>] [<DefaultValue>] val mutable bolt: GameObject

    let mutable nextFire: float32 = 0.0f

    member this.rigidBody = this.GetComponent<Rigidbody>()
    member this.audioSource = this.GetComponent<AudioSource>()

    member this.FixedUpdate () = 
        let moveHorizontal = Input.GetAxis("Horizontal")
        let moveVertical = Input.GetAxis("Vertical")

        this.rigidBody.velocity <- Vector3(moveHorizontal, 0.0f, moveVertical) * speedScale
        this.rigidBody.position <- clampWithin this.rigidBody.position this.boundary
        this.rigidBody.rotation <- Quaternion.Euler(Vector3(0.0f, 0.0f, this.rigidBody.velocity.x * -tiltFactor))
        ()

    member this.Update () =
        if Input.GetButton("Fire1") && (Time.time > nextFire)
        then 
            Object.Instantiate(this.bolt, this.shotSpawn.position, this.shotSpawn.rotation) |> ignore
            this.audioSource.Play()
            nextFire <- Time.time + fireRate

    let clampWithin (position: Vector3) (boundary: Boundary) = 
        Vector3 (
            Mathf.Clamp(position.x, boundary.xmin, boundary.xmax),
            0.0f,
            Mathf.Clamp(position.z, boundary.zmin, boundary.zmax)
        )
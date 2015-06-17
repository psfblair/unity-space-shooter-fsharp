namespace Space_Shooter

open UnityEngine
open UnityEngine.UI
open System.Collections
open System

type GameController() =
    inherit MonoBehaviour()

    [<SerializeField>] 
    let mutable hazardCount: int = 10

    [<SerializeField>] 
    let mutable startWait: float32 = 1.0f

    [<SerializeField>] 
    let mutable spawnWait: float32 = 0.5f

    [<SerializeField>] 
    let mutable waveWait: float32 = 1.0f

    [<SerializeField>] [<DefaultValue>] val mutable hazard: GameObject
    [<SerializeField>] [<DefaultValue>] val mutable spawnValue: Vector3
    [<SerializeField>] [<DefaultValue>] val mutable scoreText: Text
    [<SerializeField>] [<DefaultValue>] val mutable restartText: Text
    [<SerializeField>] [<DefaultValue>] val mutable gameOverText: Text

    let mutable score: int = 0
    let mutable isGameOver: bool = false
    let mutable shouldRestart: bool = false

    let updateScore (me: GameController) = 
        me.scoreText.text <- String.Format("Score : {0}", score)

    let initialize (me: GameController) =  
        shouldRestart <- false
        isGameOver <- false
        me.gameOverText.text <- ""
        me.restartText.text <- "Use CTRL to fire.\nUse arrow keys or\nA-D-S-W to move."
        updateScore me

    let gameNotYetOver (me: GameController) =
        if isGameOver then 
            me.restartText.text <- "Press 'R' for Restart"
            shouldRestart <- true
            false
        else true

    let spawnOneHazard (me: GameController) = 
        let spawnPosition = new Vector3(Random.Range(-me.spawnValue.x, me.spawnValue.x), me.spawnValue.y, me.spawnValue.z)
        let spawnRotation = Quaternion.identity
        Object.Instantiate(me.hazard, spawnPosition, spawnRotation)

    let spawnWaves (me: GameController) = 
        seq {
            yield WaitForSeconds(startWait)
            me.restartText.text <- ""
            while gameNotYetOver me do
                for i = 0 to Random.Range(1, hazardCount) do
                    spawnOneHazard me |> ignore
                    yield WaitForSeconds(spawnWait) 
                yield WaitForSeconds(waveWait)
            } :?> IEnumerator

    member this.Start () =
        initialize this
        this.StartCoroutine(spawnWaves this)

    member this.Update () =
        if shouldRestart && Input.GetKeyDown(KeyCode.R)
        then Application.LoadLevel(Application.loadedLevel)

    member this.AddScore (scoreValue: int) = 
        score <- score + scoreValue
        updateScore this

    member this.GameOver () = 
        isGameOver <- true
        this.gameOverText.text <- "Game Over"

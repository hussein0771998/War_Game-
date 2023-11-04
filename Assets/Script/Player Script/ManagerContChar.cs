using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ManagerContChar : MonoBehaviour
{
  
    private CharacterController _characterController;
    private float inputX, inputZ;
    private Vector3 v_movement,v_Rotate;
    public float speed;
    public ManagerJoystic MJ;
    public Animator Player;
    public ManagerContRot rot;
    public Transform player;
    private AudioSource walkStepAudioSource;
    public AudioClip walkStepAudioClip;

    private bool isAnimating = false;
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(gameObject.GetComponent<CharacterController>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        if (PV.IsMine)
        {
            
        }
       

        MJ = GameObject.Find("Joystick").GetComponent<ManagerJoystic>();
        rot = GameObject.Find("Rotation Area").GetComponent<ManagerContRot>();
        speed = 0.1f;
        _characterController = GetComponent<CharacterController>();
        walkStepAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
        //gameObject.transform.position = player.transform.position;
        /*inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");*/
        inputX = MJ.inputHorizantal();
        inputZ = MJ.inputVertical();
        if (MJ.walk != isAnimating)
        {
            isAnimating = MJ.walk;
            gameObject.GetComponent<PhotonView>().RPC("SyncAnimationState", RpcTarget.AllBuffered, isAnimating);
        }


    }
    [PunRPC]
    private void SyncAnimationState(bool newState)
    {
        isAnimating = newState;
        Player.SetBool("walk", isAnimating); // Update the Animator component.
        if (isAnimating)
        {
            // Play walking sound.
            if (!walkStepAudioSource.isPlaying)
            {
                walkStepAudioSource.PlayOneShot(walkStepAudioClip);
            }
        }
        else
        {
            // Stop walking sound.
            walkStepAudioSource.Stop();
        }
    }
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        //character move
        v_movement = new Vector3(inputX * speed, 0, inputZ * speed);
        _characterController.Move(v_movement);
        //character rotate
        if(rot.InputRotHorizontal()!=0 || rot.InputRotVertical() != 0)
        {
            v_Rotate = new Vector3(rot.InputRotHorizontal(), 0, rot.InputRotVertical());
            gameObject.GetComponent<PhotonView>().RPC("SyncPlayerRotation", RpcTarget.Others, v_Rotate);
            player.rotation = Quaternion.LookRotation(v_Rotate);
        }
       
    }
    [PunRPC]
    private void SyncPlayerRotation(Vector3 newRotation)
    {
        // Synchronize the player's rotation on all clients
        player.rotation = Quaternion.LookRotation(newRotation);
    }
}

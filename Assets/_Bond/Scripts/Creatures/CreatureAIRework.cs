using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAIRework : MonoBehaviour
{
    public BTNode behaviorTree;
    private CreatureAIContext context;
    public List<Personality> personalities = new List<Personality>();
    public Personality DefaultPersonality;

    public bool Evaluate = false;

    private void Start()
    {
        context = GetComponent<CreatureAIContext>();
    }

    private void Awake() {
        context = GetComponent<CreatureAIContext>();
    }

    private void FixedUpdate() {
        if(Evaluate){
            behaviorTree.OnParentEvaluate();
            context.animator.Move(context.agent.velocity);
        }
    }

    //build the behavior tree for the creature
    public void BuildBT() 
    {
        List<BTNode> rootList = new List<BTNode>();

        #region wild
            List<BTNode> wildSequenceList = new List<BTNode>();
            CCheckIsWild isWildCheck = new CCheckIsWild("", context);
            #region selector
                List<BTNode> wildActionSelectorList = new List<BTNode>();
                
                #region near Enemy
                    List<BTNode> wildEnemyNearSequenceList = new List<BTNode>();
                    CCheckWildEnemiesInRange enemiesInRange = new CCheckWildEnemiesInRange("", context);
                    CActionWildRunFromEnemies runFromEnemy = new CActionWildRunFromEnemies("", context);
                    wildEnemyNearSequenceList.Add(enemiesInRange);
                    wildEnemyNearSequenceList.Add(runFromEnemy);
                    BTSequence wildEnemyNearSequence = new BTSequence("", wildEnemyNearSequenceList);
                #endregion

                #region nearPlayer
                    List<BTNode> wildPlayerNearSequenceList = new List<BTNode>();
                    CCheckWildPlayerInRadius playerInNoticeRadius = new CCheckWildPlayerInRadius("", context);
                    CActionFacePlayerAndReact facePlayerAndReact = new CActionFacePlayerAndReact("", context);
                    wildPlayerNearSequenceList.Add(playerInNoticeRadius);
                    wildPlayerNearSequenceList.Add(facePlayerAndReact);
                    BTSequence wildPlayerNearSequence = new BTSequence("", wildPlayerNearSequenceList);
                #endregion

                #region tired
                    List<BTNode> wildTiredSequenceList = new List<BTNode>();
                    CCheckTirednessMeter isTired = new CCheckTirednessMeter("", context);
                    //CActionSit sit = new CActionSit("", context);
                    wildTiredSequenceList.Add(isTired);
                    // wildTiredSequenceList.Add(sit);
                    BTSequence wildTiredSequence = new BTSequence("", wildTiredSequenceList);
                #endregion

                #region bored
                    List<BTNode> wildBoredSequenceList = new List<BTNode>();
                    // CCheckBored checkBored = new CCheckBored("", context);
                        //check if bored enough
                        #region approach poi interact 
                            List<BTNode> apporachPOISequenceList = new List<BTNode>();
                            //approach poi if possible
                            //interact with poi
                            // apporachPOISequenceList.Add(Approach POI);
                            // apporachPOISequenceList.Add(Interact w POI);
                            BTSequence apporachPOISequence = new BTSequence("", apporachPOISequenceList);
                        #endregion


                    // wildBoredSequenceList.Add(checkBored);
                    // wildBoredSequenceList.Add(apporachPOISequence);
                    BTSequence wildBoredSequence = new BTSequence("", wildBoredSequenceList);
                #endregion
                
                //wandering
                //CActionWander wander = new CActionWander("", context);


                // wildActionSelectorList.Add(wildEnemyNearSequence);
                // wildActionSelectorList.Add(wildPlayerNearSequence);
                // wildActionSelectorList.Add(wildTiredSequence);
                // wildActionSelectorList.Add(wildBoredSequence);
                // wildActionSelectorList.Add(wander);
                BTSelector wildActionSelector = new BTSelector("", wildActionSelectorList);
            #endregion
            
            wildSequenceList.Add(isWildCheck);
            wildSequenceList.Add(wildActionSelector);
            BTSequence wildSequence = new BTSequence("", wildSequenceList);
        #endregion

        #region befriended
            List<BTNode> BefriendedSelectorList = new List<BTNode>();
            
            #region Whistled
                List<BTNode> WhistledSequenceList = new List<BTNode>();
                //CCheckPlayerWhistled didWhistle = new CCheckPlayerWhistled("", context);
                //CActionIncreaseAttention increaseAttention = new CActionIncreaseAttention("", context);
                // WhistledSequenceList.Add(didWhistle);
                // WhistledSequenceList.Add(increaseAttention);
                BTSequence WhistledSequence = new BTSequence("", WhistledSequenceList);
            #endregion

            #region In Combat
                List<BTNode> inCombatSequenceList = new List<BTNode>();
                CCheckInCombat inCombatCheck = new CCheckInCombat("", context);
                #region Ability Selector
                    List<BTNode> inCombatAbilitySelectorList = new List<BTNode>();
                    //CCheckIfInCombat inCombat = new CCheckIfInCombat("", context);
                    
                    #region CD Sequence
                        List<BTNode> basicOnCooldownSequenceList = new List<BTNode>();
                        //CCheckIfOnCD onCooldown = new CCheckIfOnCD ("", context);
                        //CActionMoveToEnemy moveToEnemy = new CActionMoveToEnemy("", context);
                        //basicOnCooldownSequenceList.Add(onCooldown);
                        //basicOnCooldownSequenceList.Add(moveToEnemy);
                        BTSequence basicOnCooldownSequence = new BTSequence("", basicOnCooldownSequenceList);
                    #endregion
                    
                    #region ability subtree build
                        //copy ability subtree from ability



                        
                    #endregion
                    inCombatAbilitySelectorList.Add(basicOnCooldownSequence);
                    // inCombatAbilitySelectorList.Add(abilitySubtree);
                    BTSelector inCombatAbilitySelector = new BTSelector("", inCombatAbilitySelectorList);
                #endregion
                inCombatSequenceList.Add(inCombatCheck);
                inCombatSequenceList.Add(inCombatAbilitySelector);
                BTSequence inCombatSequence = new BTSequence("", inCombatSequenceList);
            #endregion

            #region out of combat
                List<BTNode> outOfCombatSelectorList = new List<BTNode>();
                #region player moving
                    List<BTNode> playerMovingSequenceList = new List<BTNode>();
                    // CCheckPlayerMoving checkPlayerMoving = new CCheckPlayerMoving("", context);
                    // CActionFollowPlayer followPlayer = new CActionFollowPlayer("", context);
                    // playerMovingSequenceList.Add(checkPlayerMoving);
                    // playerMovingSequenceList.Add(followPlayer);
                    BTSequence playerMovingSequence = new BTSequence("", playerMovingSequenceList);
                #endregion
                
                #region player not moving
                    List<BTNode> playerNotMovingSelectorList = new List<BTNode>();
                    //player based wander action
                    // playerNotMovingSelectorList.Add(wildBoredSequence);
                    // playerNotMovingSelectorList.Add(player Wander Action);

                    BTSelector playerNotMovingSelector = new BTSelector("", playerNotMovingSelectorList);
                #endregion
                
                BTSelector outOfCombatSelector = new BTSelector("", outOfCombatSelectorList);
            #endregion
            outOfCombatSelectorList.Add(playerMovingSequence);
            outOfCombatSelectorList.Add(playerNotMovingSelector);
            BTSelector BefriendedSelector = new BTSelector("", BefriendedSelectorList);
        #endregion


        //behaviorTree = _root;
        Evaluate = true;
    }

}

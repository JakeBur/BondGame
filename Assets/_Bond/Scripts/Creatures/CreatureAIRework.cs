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

                #region Creature Frozen
                    List<BTNode> wildFrozenSequenceList = new List<BTNode>();
                    CCheckWildFrozen checkWildFrozen = new CCheckWildFrozen("", context);
                    CActionFrozen creatureFrozen = new CActionFrozen("", context);
                    wildFrozenSequenceList.Add(checkWildFrozen);
                    wildFrozenSequenceList.Add(creatureFrozen);
                    BTSequence wildFrozenSequence = new BTSequence("", wildFrozenSequenceList);
                #endregion
                
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
                    CActionSit sit = new CActionSit("", context);
                    wildTiredSequenceList.Add(isTired);
                    wildTiredSequenceList.Add(sit);
                    BTSequence wildTiredSequence = new BTSequence("", wildTiredSequenceList);
                #endregion

                #region bored
                    List<BTNode> wildBoredSequenceList = new List<BTNode>();
                    CCheckBoredomMeter checkBored = new CCheckBoredomMeter("", context);
                        #region approach poi interact 
                            List<BTNode> apporachPOISequenceList = new List<BTNode>();
                            CActionApproachPOI approachPOI = new CActionApproachPOI("", context);
                            CActionInteractWithPOI interactWithPOI = new CActionInteractWithPOI("", context);
                            apporachPOISequenceList.Add(approachPOI);
                            apporachPOISequenceList.Add(interactWithPOI);
                            BTSequence apporachPOISequence = new BTSequence("", apporachPOISequenceList);
                        #endregion

                    wildBoredSequenceList.Add(checkBored);
                    wildBoredSequenceList.Add(apporachPOISequence);
                    BTSequence wildBoredSequence = new BTSequence("", wildBoredSequenceList);
                #endregion
                
                CActionWander wander = new CActionWander("", context);

                wildActionSelectorList.Add(wildFrozenSequence);
                wildActionSelectorList.Add(wildEnemyNearSequence);
                wildActionSelectorList.Add(wildPlayerNearSequence);
                wildActionSelectorList.Add(wildTiredSequence);
                wildActionSelectorList.Add(wildBoredSequence);
                wildActionSelectorList.Add(wander);
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
                CCheckPlayerWhistled didWhistle = new CCheckPlayerWhistled("", context);
                CActionIncreaseAttention increaseAttention = new CActionIncreaseAttention("", context);
                WhistledSequenceList.Add(didWhistle);
                WhistledSequenceList.Add(increaseAttention);
                BTSequence WhistledSequence = new BTSequence("", WhistledSequenceList);
            #endregion

            #region In Combat
                List<BTNode> inCombatSequenceList = new List<BTNode>();
                CCheckInCombat inCombatCheck = new CCheckInCombat("", context);
                #region Ability Selector
                    List<BTNode> inCombatAbilitySelectorList = new List<BTNode>();

                    #region Ability Sequence
                        List<BTNode> abilityTriggeredSequenceList = new List<BTNode>();
                        CCheckPlayerTriggeredAbility playerTriggeredAbility = new CCheckPlayerTriggeredAbility("", context);
                        CCheckIfAbilityOnCd abilityOnCd = new CCheckIfAbilityOnCd("", context);
                        #region Ability Choosing Selector
                            List<BTNode> abilityChoosingSelectorList = new List<BTNode>();
                            #region Ability 1 Sequence
                                List<BTNode> abilityOneSequenceList = new List<BTNode>();
                                CCheckAbilityOne checkAbilityOne = new CCheckAbilityOne("", context);
                                abilityOneSequenceList.Add(checkAbilityOne);
                                abilityOneSequenceList.Add(context.creatureStats.abilities[0].abilityBehavior.BuildSequenceSubtree(context));
                                BTSequence AbilityOneSequence = new BTSequence("", abilityOneSequenceList);
                            #endregion

                            #region Ability 2 Sequence
                                List<BTNode> abilityTwoSequenceList = new List<BTNode>();
                                CCheckAbilityTwo checkAbilityTwo = new CCheckAbilityTwo("", context);
                                abilityTwoSequenceList.Add(checkAbilityTwo);
                                abilityTwoSequenceList.Add(context.creatureStats.abilities[1].abilityBehavior.BuildSequenceSubtree(context));
                                BTSequence AbilityTwoSequence = new BTSequence("", abilityTwoSequenceList);
                            #endregion

                            abilityChoosingSelectorList.Add(AbilityOneSequence);
                            abilityChoosingSelectorList.Add(AbilityTwoSequence);
                            BTSelector abilityChoosingSelector = new BTSelector("", abilityChoosingSelectorList);
                        #endregion

                        abilityTriggeredSequenceList.Add(playerTriggeredAbility);
                        abilityTriggeredSequenceList.Add(abilityOnCd);
                        abilityTriggeredSequenceList.Add(abilityChoosingSelector);
                        BTSequence AbilityTriggeredSequence = new BTSequence("", abilityTriggeredSequenceList);
                    #endregion

                    #region Attention Sequence
                        List<BTNode> attentionHighInCombatSequenceList = new List<BTNode>();

                        CCheckAttentionHigh attentionHigh = new CCheckAttentionHigh("", context);
                        CActionGetToPlayer getToPlayer = new CActionGetToPlayer("", context);

                        attentionHighInCombatSequenceList.Add(attentionHigh);
                        attentionHighInCombatSequenceList.Add(getToPlayer);
                        BTSequence attentionHighInCombatSequence = new BTSequence("", attentionHighInCombatSequenceList);
                    #endregion

                    #region Basic Attack Sequence
                        List<BTNode> basicAttackSequenceList = new List<BTNode>();
                        
                        CCheckDefaultAttackOnCD basicOnCD = new CCheckDefaultAttackOnCD("", context);
                        BTSequence basicAttackSubtree = context.basicCreatureAttack.abilityBehavior.BuildSequenceSubtree(context);
                        basicAttackSequenceList.Add(basicOnCD);
                        basicAttackSequenceList.Add(basicAttackSubtree);
                        
                        BTSequence basicAttackSequence = new BTSequence("", basicAttackSequenceList);
                    #endregion

                    //Combat Wander
                    CActionBefriendedWander befriendedWander = new CActionBefriendedWander("", context);

                    inCombatAbilitySelectorList.Add(AbilityTriggeredSequence);
                    inCombatAbilitySelectorList.Add(attentionHighInCombatSequence);
                    inCombatAbilitySelectorList.Add(basicAttackSequence);
                    inCombatAbilitySelectorList.Add(befriendedWander);
                    
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
                    CCheckPlayerMoving checkPlayerMoving = new CCheckPlayerMoving("", context);
                    CActionFollowPlayer followPlayer = new CActionFollowPlayer("", context);
                    playerMovingSequenceList.Add(checkPlayerMoving);
                    playerMovingSequenceList.Add(followPlayer);
                    BTSequence playerMovingSequence = new BTSequence("", playerMovingSequenceList);
                #endregion
                
                #region player not moving
                    List<BTNode> playerNotMovingSelectorList = new List<BTNode>();

                    //CActionBefriendedWander befriendedWander = new CActionBefriendedWander("", context);

                    //playerNotMovingSelectorList.Add(wildBoredSequence);
                    playerNotMovingSelectorList.Add(befriendedWander);

                    BTSelector playerNotMovingSelector = new BTSelector("", playerNotMovingSelectorList);
                #endregion

                outOfCombatSelectorList.Add(playerMovingSequence);
                outOfCombatSelectorList.Add(playerNotMovingSelector);
                BTSelector outOfCombatSelector = new BTSelector("", outOfCombatSelectorList);
            #endregion

            //BefriendedSelectorList.Add(WhistledSequence);
            BefriendedSelectorList.Add(inCombatSequence);
            BefriendedSelectorList.Add(outOfCombatSelector);
            
            BTSelector BefriendedSelector = new BTSelector("", BefriendedSelectorList);
        #endregion

        rootList.Add(wildSequence);
        rootList.Add(BefriendedSelector);
        BTSelector root = new BTSelector("", rootList);
        behaviorTree = root;
        Evaluate = true;
    }

}

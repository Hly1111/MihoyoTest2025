using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyAttackPerception : EnemyAIBrain
{ 
        private float _rand;
        protected override void InitializeInternal()
        {
                base.InitializeInternal();
                EnemyAIKnowledge.attackKnowledge.canAttack = true;
                _rand = Random.Range(EnemyAIKnowledge.aiController.EnemyAIData.MinAttackDuration, EnemyAIKnowledge.aiController.EnemyAIData.MaxAttackDuration);
        }
        
        protected override void UpdateDecisionInternal()
        {
                base.UpdateDecisionInternal();
                HandleAttack();
        }

        private void HandleAttack()
        {
                if(!EnemyAIKnowledge.targetKnowledge.target)
                {
                        return;
                }
                
                EnemyAIKnowledge.attackKnowledge.timer += Time.deltaTime;
                if (EnemyAIKnowledge.attackKnowledge.timer >= EnemyAIKnowledge.aiController.EnemyAIData.MinAttackDuration)
                {
                        if(EnemyAIKnowledge.attackKnowledge.timer >= _rand)
                        {
                                EnemyAIKnowledge.attackKnowledge.shouldAttack = true;
                        }
                }
        }

        public void ResetAttack()
        {
                _rand = Random.Range(EnemyAIKnowledge.aiController.EnemyAIData.MinAttackDuration, EnemyAIKnowledge.aiController.EnemyAIData.MaxAttackDuration);
                EnemyAIKnowledge.attackKnowledge.shouldAttack = false;
                EnemyAIKnowledge.attackKnowledge.timer = 0;
        }
}
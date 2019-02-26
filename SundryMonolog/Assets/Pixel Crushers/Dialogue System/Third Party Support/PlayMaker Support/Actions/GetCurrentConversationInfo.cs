using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker
{

    [ActionCategory("Dialogue System")]
    [HutongGames.PlayMaker.TooltipAttribute("Gets info about the currently active conversation.")]
    public class GetCurrentConversationInfo : FsmStateAction
    {

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.TooltipAttribute("Store the conversation title in a String variable")]
        public FsmString conversationTitle;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.TooltipAttribute("Store the conversation ID in an Int variable")]
        public FsmInt conversationID;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.TooltipAttribute("Store the dialogue entry ID in an Int variable")]
        public FsmInt entryID;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.TooltipAttribute("Store the actor name in a String variable")]
        public FsmString actorName;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.TooltipAttribute("Store the conversant name in a String variable")]
        public FsmString conversantName;

        public override void Reset()
        {
            conversationTitle = null;
            conversationID = null;
            entryID = null;
            actorName = null;
            conversantName = null;
        }

        public override void OnEnter()
        {
            var isActive = DialogueManager.isConversationActive;
            var state = isActive ? DialogueManager.currentConversationState : null;
            if (conversationTitle != null)
            {
                var conversation = isActive ? DialogueManager.MasterDatabase.GetConversation(state.subtitle.dialogueEntry.conversationID) : null;
                conversationTitle.Value = (conversation != null) ? conversation.Title : string.Empty;
            }
            if (conversationID != null) conversationID.Value = isActive ? state.subtitle.dialogueEntry.conversationID : 0;
            if (entryID != null) entryID.Value = isActive ? state.subtitle.dialogueEntry.id : 0;
            if (actorName != null) actorName.Value = DialogueLua.GetVariable("Actor").AsString;
            if (conversantName != null) conversantName.Value = DialogueLua.GetVariable("Conversant").AsString;
            Finish();
        }

    }

}
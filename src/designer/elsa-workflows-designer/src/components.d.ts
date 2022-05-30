/* eslint-disable */
/* tslint:disable */
/**
 * This is an autogenerated file created by the Stencil compiler.
 * It contains typing information for all components that exist in this project.
 */
import { HTMLStencilElement, JSXBase } from "@stencil/core/internal";
import { ActionDefinition, ActionInvokedArgs, Activity, ActivityDescriptor, ActivitySelectedArgs, ContainerSelectedArgs, GraphUpdatedArgs, IntellisenseContext, SelectListItem, TabChangedArgs, TabDefinition, WorkflowDefinition, WorkflowDefinitionSummary, WorkflowInstance, WorkflowInstanceSummary } from "./models";
import { ActivityUpdatedArgs, DeleteActivityRequestedArgs } from "./components/designer/workflow-definition-editor/activity-properties-editor";
import { Button } from "./components/shared/button-group/models";
import { ContainerActivityComponent } from "./components/activities/container-activity-component";
import { AddActivityArgs } from "./components/designer/canvas/canvas";
import { ActivityInputContext } from "./services/node-input-driver";
import { ContextMenuAnchorPoint, MenuItem, MenuItemGroup } from "./components/shared/context-menu/models";
import { DropdownButtonItem, DropdownButtonOrigin } from "./components/shared/dropdown-button/models";
import { Graph } from "@antv/x6";
import { AddActivityArgs as AddActivityArgs1 } from "./components/designer/canvas/canvas";
import { ExpressionChangedArs } from "./components/designer/input-control-switch/input-control-switch";
import { CreateLabelEventArgs, DeleteLabelEventArgs, Label, UpdateLabelEventArgs } from "./modules/labels/models";
import { MonacoLib, MonacoValueChangedArgs } from "./components/shared/monaco-editor/monaco-editor";
import { PagerData } from "./components/shared/pager/pager";
import { PanelPosition, PanelStateChangedArgs } from "./components/designer/panel/models";
import { WorkflowDefinitionPropsUpdatedArgs, WorkflowDefinitionUpdatedArgs } from "./components/designer/workflow-definition-editor/models";
import { ActivityDriverRegistry } from "./services";
import { Flowchart } from "./components/activities/flowchart/models";
import { PublishClickedArgs } from "./components/toolbar/workflow-publish-button/workflow-publish-button";
export namespace Components {
    interface ElsaActivityPropertiesEditor {
        "activity"?: Activity;
        "activityDescriptors": Array<ActivityDescriptor>;
        "hide": () => Promise<void>;
        "show": () => Promise<void>;
    }
    interface ElsaButtonGroup {
        "buttons": Array<Button>;
    }
    interface ElsaCanvas {
        "addActivity": (args: AddActivityArgs) => Promise<void>;
        "exportGraph": () => Promise<Activity>;
        "getRootComponent": () => Promise<ContainerActivityComponent>;
        "importGraph": (root: Activity) => Promise<void>;
        "interactiveMode": boolean;
        "updateLayout": () => Promise<void>;
    }
    interface ElsaCheckListInput {
        "inputContext": ActivityInputContext;
    }
    interface ElsaCodeEditorInput {
        "inputContext": ActivityInputContext;
    }
    interface ElsaContextMenu {
        "anchorPoint": ContextMenuAnchorPoint;
        "close": () => Promise<void>;
        "hideButton": boolean;
        "menuItemGroups": Array<MenuItemGroup>;
        "menuItems": Array<MenuItem>;
        "open": () => Promise<void>;
    }
    interface ElsaDropdownButton {
        "icon"?: any;
        "items": Array<DropdownButtonItem>;
        "origin": DropdownButtonOrigin;
        "text": string;
    }
    interface ElsaDropdownInput {
        "inputContext": ActivityInputContext;
    }
    interface ElsaFlowchart {
        "activityDescriptors": Array<ActivityDescriptor>;
        "addActivity": (args: AddActivityArgs) => Promise<void>;
        "exportRoot": () => Promise<Activity>;
        "getGraph": () => Promise<Graph>;
        "importRoot": (root: Activity) => Promise<void>;
        "interactiveMode": boolean;
        "root"?: Activity;
        "updateLayout": () => Promise<void>;
    }
    interface ElsaFormPanel {
        "actions": Array<ActionDefinition>;
        "headerText": string;
        "selectedTabIndex"?: number;
        "tabs": Array<TabDefinition>;
    }
    interface ElsaInputControlSwitch {
        "codeEditorHeight": string;
        "codeEditorSingleLineMode": boolean;
        "context"?: IntellisenseContext;
        "defaultSyntax": string;
        "expression"?: string;
        "fieldName"?: string;
        "hint": string;
        "isReadOnly"?: boolean;
        "label": string;
        "supportedSyntaxes": Array<string>;
        "syntax"?: string;
    }
    interface ElsaInputTags {
        "fieldId"?: string;
        "placeHolder"?: string;
        "values"?: Array<string>;
    }
    interface ElsaInputTagsDropdown {
        "dropdownValues"?: Array<SelectListItem>;
        "fieldId"?: string;
        "fieldName"?: string;
        "placeHolder"?: string;
        "values"?: Array<string | SelectListItem>;
    }
    interface ElsaLabelCreator {
    }
    interface ElsaLabelEditor {
        "label": Label;
    }
    interface ElsaLabelPicker {
        "selectedLabels": Array<string>;
    }
    interface ElsaLabelsManager {
        "hide": () => Promise<void>;
        "show": () => Promise<void>;
    }
    interface ElsaLabelsWidget {
    }
    interface ElsaModalDialog {
        "actions": Array<ActionDefinition>;
        "hide": (animate?: boolean) => Promise<void>;
        "show": (animate?: boolean) => Promise<void>;
        "size": string;
    }
    interface ElsaMonacoEditor {
        "editorHeight": string;
        "language": string;
        "monacoLibPath"?: string;
        "padding": string;
        "setJavaScriptLibs": (libs: Array<MonacoLib>) => Promise<void>;
        "singleLineMode": boolean;
        "value": string;
    }
    interface ElsaMultiLineInput {
        "inputContext": ActivityInputContext;
    }
    interface ElsaMultiTextInput {
        "inputContext": ActivityInputContext;
    }
    interface ElsaPager {
        "page": number;
        "pageSize": number;
        "totalCount": number;
    }
    interface ElsaPanel {
        "position": PanelPosition;
    }
    interface ElsaRadioListInput {
        "inputContext": ActivityInputContext;
    }
    interface ElsaSingleLineInput {
        "inputContext": ActivityInputContext;
    }
    interface ElsaSlideOverPanel {
        "actions": Array<ActionDefinition>;
        "expand": boolean;
        "headerText": string;
        "hide": () => Promise<void>;
        "selectedTab"?: TabDefinition;
        "show": () => Promise<void>;
        "tabs": Array<TabDefinition>;
    }
    interface ElsaStudio {
        "monacoLibPath": string;
        "serverUrl": string;
    }
    interface ElsaSwitchEditor {
        "inputContext": ActivityInputContext;
    }
    interface ElsaTooltip {
        "tooltipContent": any;
        "tooltipPosition"?: string;
    }
    interface ElsaWorkflowDefinitionBrowser {
        "hide": () => Promise<void>;
        "show": () => Promise<void>;
    }
    interface ElsaWorkflowDefinitionEditor {
        "activityDescriptors": Array<ActivityDescriptor>;
        "getCanvas": () => Promise<HTMLElsaCanvasElement>;
        "getWorkflowDefinition": () => Promise<WorkflowDefinition>;
        "importWorkflow": (workflowDefinition: WorkflowDefinition) => Promise<void>;
        "monacoLibPath": string;
        "newWorkflow": () => Promise<void>;
        "registerActivityDrivers": (register: (registry: ActivityDriverRegistry) => void) => Promise<void>;
        "updateWorkflowDefinition": (workflowDefinition: WorkflowDefinition) => Promise<void>;
        "workflowDefinition"?: WorkflowDefinition;
    }
    interface ElsaWorkflowDefinitionEditorToolbox {
        "graph": Graph;
    }
    interface ElsaWorkflowDefinitionEditorToolboxActivities {
        "activityDescriptors": Array<ActivityDescriptor>;
        "graph": Graph;
    }
    interface ElsaWorkflowDefinitionPropertiesEditor {
        "hide": () => Promise<void>;
        "show": () => Promise<void>;
        "workflowDefinition"?: WorkflowDefinition;
    }
    interface ElsaWorkflowInstanceBrowser {
        "hide": () => Promise<void>;
        "show": () => Promise<void>;
    }
    interface ElsaWorkflowInstanceProperties {
        "hide": () => Promise<void>;
        "show": () => Promise<void>;
        "workflowDefinition"?: WorkflowDefinition;
    }
    interface ElsaWorkflowInstanceViewer {
        "activityDescriptors": Array<ActivityDescriptor>;
        "getCanvas": () => Promise<HTMLElsaCanvasElement>;
        "getWorkflow": () => Promise<WorkflowDefinition>;
        "importWorkflow": (workflowDefinition: WorkflowDefinition, workflowInstance?: WorkflowInstance) => Promise<void>;
        "monacoLibPath": string;
        "registerActivityDrivers": (register: (registry: ActivityDriverRegistry) => void) => Promise<void>;
        "updateWorkflowDefinition": (workflowDefinition: WorkflowDefinition) => Promise<void>;
        "workflowDefinition": WorkflowDefinition;
        "workflowInstance": WorkflowInstance;
    }
    interface ElsaWorkflowManager {
        "activityDescriptors": Array<ActivityDescriptor>;
        "getWorkflowDefinition": () => Promise<WorkflowDefinition>;
        "monacoLibPath": string;
        /**
          * Updates the workflow definition without importing it into the designer.
         */
        "updateWorkflowDefinition": (workflowDefinition: WorkflowDefinition) => Promise<void>;
        "workflowDefinition"?: WorkflowDefinition;
        "workflowInstance"?: WorkflowInstance;
    }
    interface ElsaWorkflowPublishButton {
        "publishing": boolean;
    }
    interface ElsaWorkflowToolbar {
    }
    interface ElsaWorkflowToolbarMenu {
    }
}
declare global {
    interface HTMLElsaActivityPropertiesEditorElement extends Components.ElsaActivityPropertiesEditor, HTMLStencilElement {
    }
    var HTMLElsaActivityPropertiesEditorElement: {
        prototype: HTMLElsaActivityPropertiesEditorElement;
        new (): HTMLElsaActivityPropertiesEditorElement;
    };
    interface HTMLElsaButtonGroupElement extends Components.ElsaButtonGroup, HTMLStencilElement {
    }
    var HTMLElsaButtonGroupElement: {
        prototype: HTMLElsaButtonGroupElement;
        new (): HTMLElsaButtonGroupElement;
    };
    interface HTMLElsaCanvasElement extends Components.ElsaCanvas, HTMLStencilElement {
    }
    var HTMLElsaCanvasElement: {
        prototype: HTMLElsaCanvasElement;
        new (): HTMLElsaCanvasElement;
    };
    interface HTMLElsaCheckListInputElement extends Components.ElsaCheckListInput, HTMLStencilElement {
    }
    var HTMLElsaCheckListInputElement: {
        prototype: HTMLElsaCheckListInputElement;
        new (): HTMLElsaCheckListInputElement;
    };
    interface HTMLElsaCodeEditorInputElement extends Components.ElsaCodeEditorInput, HTMLStencilElement {
    }
    var HTMLElsaCodeEditorInputElement: {
        prototype: HTMLElsaCodeEditorInputElement;
        new (): HTMLElsaCodeEditorInputElement;
    };
    interface HTMLElsaContextMenuElement extends Components.ElsaContextMenu, HTMLStencilElement {
    }
    var HTMLElsaContextMenuElement: {
        prototype: HTMLElsaContextMenuElement;
        new (): HTMLElsaContextMenuElement;
    };
    interface HTMLElsaDropdownButtonElement extends Components.ElsaDropdownButton, HTMLStencilElement {
    }
    var HTMLElsaDropdownButtonElement: {
        prototype: HTMLElsaDropdownButtonElement;
        new (): HTMLElsaDropdownButtonElement;
    };
    interface HTMLElsaDropdownInputElement extends Components.ElsaDropdownInput, HTMLStencilElement {
    }
    var HTMLElsaDropdownInputElement: {
        prototype: HTMLElsaDropdownInputElement;
        new (): HTMLElsaDropdownInputElement;
    };
    interface HTMLElsaFlowchartElement extends Components.ElsaFlowchart, HTMLStencilElement {
    }
    var HTMLElsaFlowchartElement: {
        prototype: HTMLElsaFlowchartElement;
        new (): HTMLElsaFlowchartElement;
    };
    interface HTMLElsaFormPanelElement extends Components.ElsaFormPanel, HTMLStencilElement {
    }
    var HTMLElsaFormPanelElement: {
        prototype: HTMLElsaFormPanelElement;
        new (): HTMLElsaFormPanelElement;
    };
    interface HTMLElsaInputControlSwitchElement extends Components.ElsaInputControlSwitch, HTMLStencilElement {
    }
    var HTMLElsaInputControlSwitchElement: {
        prototype: HTMLElsaInputControlSwitchElement;
        new (): HTMLElsaInputControlSwitchElement;
    };
    interface HTMLElsaInputTagsElement extends Components.ElsaInputTags, HTMLStencilElement {
    }
    var HTMLElsaInputTagsElement: {
        prototype: HTMLElsaInputTagsElement;
        new (): HTMLElsaInputTagsElement;
    };
    interface HTMLElsaInputTagsDropdownElement extends Components.ElsaInputTagsDropdown, HTMLStencilElement {
    }
    var HTMLElsaInputTagsDropdownElement: {
        prototype: HTMLElsaInputTagsDropdownElement;
        new (): HTMLElsaInputTagsDropdownElement;
    };
    interface HTMLElsaLabelCreatorElement extends Components.ElsaLabelCreator, HTMLStencilElement {
    }
    var HTMLElsaLabelCreatorElement: {
        prototype: HTMLElsaLabelCreatorElement;
        new (): HTMLElsaLabelCreatorElement;
    };
    interface HTMLElsaLabelEditorElement extends Components.ElsaLabelEditor, HTMLStencilElement {
    }
    var HTMLElsaLabelEditorElement: {
        prototype: HTMLElsaLabelEditorElement;
        new (): HTMLElsaLabelEditorElement;
    };
    interface HTMLElsaLabelPickerElement extends Components.ElsaLabelPicker, HTMLStencilElement {
    }
    var HTMLElsaLabelPickerElement: {
        prototype: HTMLElsaLabelPickerElement;
        new (): HTMLElsaLabelPickerElement;
    };
    interface HTMLElsaLabelsManagerElement extends Components.ElsaLabelsManager, HTMLStencilElement {
    }
    var HTMLElsaLabelsManagerElement: {
        prototype: HTMLElsaLabelsManagerElement;
        new (): HTMLElsaLabelsManagerElement;
    };
    interface HTMLElsaLabelsWidgetElement extends Components.ElsaLabelsWidget, HTMLStencilElement {
    }
    var HTMLElsaLabelsWidgetElement: {
        prototype: HTMLElsaLabelsWidgetElement;
        new (): HTMLElsaLabelsWidgetElement;
    };
    interface HTMLElsaModalDialogElement extends Components.ElsaModalDialog, HTMLStencilElement {
    }
    var HTMLElsaModalDialogElement: {
        prototype: HTMLElsaModalDialogElement;
        new (): HTMLElsaModalDialogElement;
    };
    interface HTMLElsaMonacoEditorElement extends Components.ElsaMonacoEditor, HTMLStencilElement {
    }
    var HTMLElsaMonacoEditorElement: {
        prototype: HTMLElsaMonacoEditorElement;
        new (): HTMLElsaMonacoEditorElement;
    };
    interface HTMLElsaMultiLineInputElement extends Components.ElsaMultiLineInput, HTMLStencilElement {
    }
    var HTMLElsaMultiLineInputElement: {
        prototype: HTMLElsaMultiLineInputElement;
        new (): HTMLElsaMultiLineInputElement;
    };
    interface HTMLElsaMultiTextInputElement extends Components.ElsaMultiTextInput, HTMLStencilElement {
    }
    var HTMLElsaMultiTextInputElement: {
        prototype: HTMLElsaMultiTextInputElement;
        new (): HTMLElsaMultiTextInputElement;
    };
    interface HTMLElsaPagerElement extends Components.ElsaPager, HTMLStencilElement {
    }
    var HTMLElsaPagerElement: {
        prototype: HTMLElsaPagerElement;
        new (): HTMLElsaPagerElement;
    };
    interface HTMLElsaPanelElement extends Components.ElsaPanel, HTMLStencilElement {
    }
    var HTMLElsaPanelElement: {
        prototype: HTMLElsaPanelElement;
        new (): HTMLElsaPanelElement;
    };
    interface HTMLElsaRadioListInputElement extends Components.ElsaRadioListInput, HTMLStencilElement {
    }
    var HTMLElsaRadioListInputElement: {
        prototype: HTMLElsaRadioListInputElement;
        new (): HTMLElsaRadioListInputElement;
    };
    interface HTMLElsaSingleLineInputElement extends Components.ElsaSingleLineInput, HTMLStencilElement {
    }
    var HTMLElsaSingleLineInputElement: {
        prototype: HTMLElsaSingleLineInputElement;
        new (): HTMLElsaSingleLineInputElement;
    };
    interface HTMLElsaSlideOverPanelElement extends Components.ElsaSlideOverPanel, HTMLStencilElement {
    }
    var HTMLElsaSlideOverPanelElement: {
        prototype: HTMLElsaSlideOverPanelElement;
        new (): HTMLElsaSlideOverPanelElement;
    };
    interface HTMLElsaStudioElement extends Components.ElsaStudio, HTMLStencilElement {
    }
    var HTMLElsaStudioElement: {
        prototype: HTMLElsaStudioElement;
        new (): HTMLElsaStudioElement;
    };
    interface HTMLElsaSwitchEditorElement extends Components.ElsaSwitchEditor, HTMLStencilElement {
    }
    var HTMLElsaSwitchEditorElement: {
        prototype: HTMLElsaSwitchEditorElement;
        new (): HTMLElsaSwitchEditorElement;
    };
    interface HTMLElsaTooltipElement extends Components.ElsaTooltip, HTMLStencilElement {
    }
    var HTMLElsaTooltipElement: {
        prototype: HTMLElsaTooltipElement;
        new (): HTMLElsaTooltipElement;
    };
    interface HTMLElsaWorkflowDefinitionBrowserElement extends Components.ElsaWorkflowDefinitionBrowser, HTMLStencilElement {
    }
    var HTMLElsaWorkflowDefinitionBrowserElement: {
        prototype: HTMLElsaWorkflowDefinitionBrowserElement;
        new (): HTMLElsaWorkflowDefinitionBrowserElement;
    };
    interface HTMLElsaWorkflowDefinitionEditorElement extends Components.ElsaWorkflowDefinitionEditor, HTMLStencilElement {
    }
    var HTMLElsaWorkflowDefinitionEditorElement: {
        prototype: HTMLElsaWorkflowDefinitionEditorElement;
        new (): HTMLElsaWorkflowDefinitionEditorElement;
    };
    interface HTMLElsaWorkflowDefinitionEditorToolboxElement extends Components.ElsaWorkflowDefinitionEditorToolbox, HTMLStencilElement {
    }
    var HTMLElsaWorkflowDefinitionEditorToolboxElement: {
        prototype: HTMLElsaWorkflowDefinitionEditorToolboxElement;
        new (): HTMLElsaWorkflowDefinitionEditorToolboxElement;
    };
    interface HTMLElsaWorkflowDefinitionEditorToolboxActivitiesElement extends Components.ElsaWorkflowDefinitionEditorToolboxActivities, HTMLStencilElement {
    }
    var HTMLElsaWorkflowDefinitionEditorToolboxActivitiesElement: {
        prototype: HTMLElsaWorkflowDefinitionEditorToolboxActivitiesElement;
        new (): HTMLElsaWorkflowDefinitionEditorToolboxActivitiesElement;
    };
    interface HTMLElsaWorkflowDefinitionPropertiesEditorElement extends Components.ElsaWorkflowDefinitionPropertiesEditor, HTMLStencilElement {
    }
    var HTMLElsaWorkflowDefinitionPropertiesEditorElement: {
        prototype: HTMLElsaWorkflowDefinitionPropertiesEditorElement;
        new (): HTMLElsaWorkflowDefinitionPropertiesEditorElement;
    };
    interface HTMLElsaWorkflowInstanceBrowserElement extends Components.ElsaWorkflowInstanceBrowser, HTMLStencilElement {
    }
    var HTMLElsaWorkflowInstanceBrowserElement: {
        prototype: HTMLElsaWorkflowInstanceBrowserElement;
        new (): HTMLElsaWorkflowInstanceBrowserElement;
    };
    interface HTMLElsaWorkflowInstancePropertiesElement extends Components.ElsaWorkflowInstanceProperties, HTMLStencilElement {
    }
    var HTMLElsaWorkflowInstancePropertiesElement: {
        prototype: HTMLElsaWorkflowInstancePropertiesElement;
        new (): HTMLElsaWorkflowInstancePropertiesElement;
    };
    interface HTMLElsaWorkflowInstanceViewerElement extends Components.ElsaWorkflowInstanceViewer, HTMLStencilElement {
    }
    var HTMLElsaWorkflowInstanceViewerElement: {
        prototype: HTMLElsaWorkflowInstanceViewerElement;
        new (): HTMLElsaWorkflowInstanceViewerElement;
    };
    interface HTMLElsaWorkflowManagerElement extends Components.ElsaWorkflowManager, HTMLStencilElement {
    }
    var HTMLElsaWorkflowManagerElement: {
        prototype: HTMLElsaWorkflowManagerElement;
        new (): HTMLElsaWorkflowManagerElement;
    };
    interface HTMLElsaWorkflowPublishButtonElement extends Components.ElsaWorkflowPublishButton, HTMLStencilElement {
    }
    var HTMLElsaWorkflowPublishButtonElement: {
        prototype: HTMLElsaWorkflowPublishButtonElement;
        new (): HTMLElsaWorkflowPublishButtonElement;
    };
    interface HTMLElsaWorkflowToolbarElement extends Components.ElsaWorkflowToolbar, HTMLStencilElement {
    }
    var HTMLElsaWorkflowToolbarElement: {
        prototype: HTMLElsaWorkflowToolbarElement;
        new (): HTMLElsaWorkflowToolbarElement;
    };
    interface HTMLElsaWorkflowToolbarMenuElement extends Components.ElsaWorkflowToolbarMenu, HTMLStencilElement {
    }
    var HTMLElsaWorkflowToolbarMenuElement: {
        prototype: HTMLElsaWorkflowToolbarMenuElement;
        new (): HTMLElsaWorkflowToolbarMenuElement;
    };
    interface HTMLElementTagNameMap {
        "elsa-activity-properties-editor": HTMLElsaActivityPropertiesEditorElement;
        "elsa-button-group": HTMLElsaButtonGroupElement;
        "elsa-canvas": HTMLElsaCanvasElement;
        "elsa-check-list-input": HTMLElsaCheckListInputElement;
        "elsa-code-editor-input": HTMLElsaCodeEditorInputElement;
        "elsa-context-menu": HTMLElsaContextMenuElement;
        "elsa-dropdown-button": HTMLElsaDropdownButtonElement;
        "elsa-dropdown-input": HTMLElsaDropdownInputElement;
        "elsa-flowchart": HTMLElsaFlowchartElement;
        "elsa-form-panel": HTMLElsaFormPanelElement;
        "elsa-input-control-switch": HTMLElsaInputControlSwitchElement;
        "elsa-input-tags": HTMLElsaInputTagsElement;
        "elsa-input-tags-dropdown": HTMLElsaInputTagsDropdownElement;
        "elsa-label-creator": HTMLElsaLabelCreatorElement;
        "elsa-label-editor": HTMLElsaLabelEditorElement;
        "elsa-label-picker": HTMLElsaLabelPickerElement;
        "elsa-labels-manager": HTMLElsaLabelsManagerElement;
        "elsa-labels-widget": HTMLElsaLabelsWidgetElement;
        "elsa-modal-dialog": HTMLElsaModalDialogElement;
        "elsa-monaco-editor": HTMLElsaMonacoEditorElement;
        "elsa-multi-line-input": HTMLElsaMultiLineInputElement;
        "elsa-multi-text-input": HTMLElsaMultiTextInputElement;
        "elsa-pager": HTMLElsaPagerElement;
        "elsa-panel": HTMLElsaPanelElement;
        "elsa-radio-list-input": HTMLElsaRadioListInputElement;
        "elsa-single-line-input": HTMLElsaSingleLineInputElement;
        "elsa-slide-over-panel": HTMLElsaSlideOverPanelElement;
        "elsa-studio": HTMLElsaStudioElement;
        "elsa-switch-editor": HTMLElsaSwitchEditorElement;
        "elsa-tooltip": HTMLElsaTooltipElement;
        "elsa-workflow-definition-browser": HTMLElsaWorkflowDefinitionBrowserElement;
        "elsa-workflow-definition-editor": HTMLElsaWorkflowDefinitionEditorElement;
        "elsa-workflow-definition-editor-toolbox": HTMLElsaWorkflowDefinitionEditorToolboxElement;
        "elsa-workflow-definition-editor-toolbox-activities": HTMLElsaWorkflowDefinitionEditorToolboxActivitiesElement;
        "elsa-workflow-definition-properties-editor": HTMLElsaWorkflowDefinitionPropertiesEditorElement;
        "elsa-workflow-instance-browser": HTMLElsaWorkflowInstanceBrowserElement;
        "elsa-workflow-instance-properties": HTMLElsaWorkflowInstancePropertiesElement;
        "elsa-workflow-instance-viewer": HTMLElsaWorkflowInstanceViewerElement;
        "elsa-workflow-manager": HTMLElsaWorkflowManagerElement;
        "elsa-workflow-publish-button": HTMLElsaWorkflowPublishButtonElement;
        "elsa-workflow-toolbar": HTMLElsaWorkflowToolbarElement;
        "elsa-workflow-toolbar-menu": HTMLElsaWorkflowToolbarMenuElement;
    }
}
declare namespace LocalJSX {
    interface ElsaActivityPropertiesEditor {
        "activity"?: Activity;
        "activityDescriptors"?: Array<ActivityDescriptor>;
        "onActivityUpdated"?: (event: CustomEvent<ActivityUpdatedArgs>) => void;
        "onDeleteActivityRequested"?: (event: CustomEvent<DeleteActivityRequestedArgs>) => void;
    }
    interface ElsaButtonGroup {
        "buttons"?: Array<Button>;
    }
    interface ElsaCanvas {
        "interactiveMode"?: boolean;
    }
    interface ElsaCheckListInput {
        "inputContext"?: ActivityInputContext;
    }
    interface ElsaCodeEditorInput {
        "inputContext"?: ActivityInputContext;
    }
    interface ElsaContextMenu {
        "anchorPoint"?: ContextMenuAnchorPoint;
        "hideButton"?: boolean;
        "menuItemGroups"?: Array<MenuItemGroup>;
        "menuItems"?: Array<MenuItem>;
    }
    interface ElsaDropdownButton {
        "icon"?: any;
        "items"?: Array<DropdownButtonItem>;
        "onItemSelected"?: (event: CustomEvent<DropdownButtonItem>) => void;
        "origin"?: DropdownButtonOrigin;
        "text"?: string;
    }
    interface ElsaDropdownInput {
        "inputContext"?: ActivityInputContext;
    }
    interface ElsaFlowchart {
        "activityDescriptors"?: Array<ActivityDescriptor>;
        "interactiveMode"?: boolean;
        "onActivitySelected"?: (event: CustomEvent<ActivitySelectedArgs>) => void;
        "onContainerSelected"?: (event: CustomEvent<ContainerSelectedArgs>) => void;
        "onGraphUpdated"?: (event: CustomEvent<GraphUpdatedArgs>) => void;
        "root"?: Activity;
    }
    interface ElsaFormPanel {
        "actions"?: Array<ActionDefinition>;
        "headerText"?: string;
        "onActionInvoked"?: (event: CustomEvent<ActionInvokedArgs>) => void;
        "onSelectedTabIndexChanged"?: (event: CustomEvent<TabChangedArgs>) => void;
        "onSubmitted"?: (event: CustomEvent<FormData>) => void;
        "selectedTabIndex"?: number;
        "tabs"?: Array<TabDefinition>;
    }
    interface ElsaInputControlSwitch {
        "codeEditorHeight"?: string;
        "codeEditorSingleLineMode"?: boolean;
        "context"?: IntellisenseContext;
        "defaultSyntax"?: string;
        "expression"?: string;
        "fieldName"?: string;
        "hint"?: string;
        "isReadOnly"?: boolean;
        "label"?: string;
        "onExpressionChanged"?: (event: CustomEvent<ExpressionChangedArs>) => void;
        "onSyntaxChanged"?: (event: CustomEvent<string>) => void;
        "supportedSyntaxes"?: Array<string>;
        "syntax"?: string;
    }
    interface ElsaInputTags {
        "fieldId"?: string;
        "onValueChanged"?: (event: CustomEvent<Array<string>>) => void;
        "placeHolder"?: string;
        "values"?: Array<string>;
    }
    interface ElsaInputTagsDropdown {
        "dropdownValues"?: Array<SelectListItem>;
        "fieldId"?: string;
        "fieldName"?: string;
        "onValueChanged"?: (event: CustomEvent<Array<string | SelectListItem>>) => void;
        "placeHolder"?: string;
        "values"?: Array<string | SelectListItem>;
    }
    interface ElsaLabelCreator {
        "onCreateLabelClicked"?: (event: CustomEvent<CreateLabelEventArgs>) => void;
    }
    interface ElsaLabelEditor {
        "label"?: Label;
        "onLabelDeleted"?: (event: CustomEvent<DeleteLabelEventArgs>) => void;
        "onLabelUpdated"?: (event: CustomEvent<UpdateLabelEventArgs>) => void;
    }
    interface ElsaLabelPicker {
        "onSelectedLabelsChanged"?: (event: CustomEvent<Array<string>>) => void;
        "selectedLabels"?: Array<string>;
    }
    interface ElsaLabelsManager {
    }
    interface ElsaLabelsWidget {
    }
    interface ElsaModalDialog {
        "actions"?: Array<ActionDefinition>;
        "onActionInvoked"?: (event: CustomEvent<ActionInvokedArgs>) => void;
        "onHidden"?: (event: CustomEvent<any>) => void;
        "onShown"?: (event: CustomEvent<any>) => void;
        "size"?: string;
    }
    interface ElsaMonacoEditor {
        "editorHeight"?: string;
        "language"?: string;
        "monacoLibPath"?: string;
        "onValueChanged"?: (event: CustomEvent<MonacoValueChangedArgs>) => void;
        "padding"?: string;
        "singleLineMode"?: boolean;
        "value"?: string;
    }
    interface ElsaMultiLineInput {
        "inputContext"?: ActivityInputContext;
    }
    interface ElsaMultiTextInput {
        "inputContext"?: ActivityInputContext;
    }
    interface ElsaPager {
        "onPaginated"?: (event: CustomEvent<PagerData>) => void;
        "page"?: number;
        "pageSize"?: number;
        "totalCount"?: number;
    }
    interface ElsaPanel {
        "onExpandedStateChanged"?: (event: CustomEvent<PanelStateChangedArgs>) => void;
        "position"?: PanelPosition;
    }
    interface ElsaRadioListInput {
        "inputContext"?: ActivityInputContext;
    }
    interface ElsaSingleLineInput {
        "inputContext"?: ActivityInputContext;
    }
    interface ElsaSlideOverPanel {
        "actions"?: Array<ActionDefinition>;
        "expand"?: boolean;
        "headerText"?: string;
        "onCollapsed"?: (event: CustomEvent<any>) => void;
        "onSubmitted"?: (event: CustomEvent<FormData>) => void;
        "selectedTab"?: TabDefinition;
        "tabs"?: Array<TabDefinition>;
    }
    interface ElsaStudio {
        "monacoLibPath"?: string;
        "serverUrl"?: string;
    }
    interface ElsaSwitchEditor {
        "inputContext"?: ActivityInputContext;
    }
    interface ElsaTooltip {
        "tooltipContent"?: any;
        "tooltipPosition"?: string;
    }
    interface ElsaWorkflowDefinitionBrowser {
        "onWorkflowDefinitionSelected"?: (event: CustomEvent<WorkflowDefinitionSummary>) => void;
    }
    interface ElsaWorkflowDefinitionEditor {
        "activityDescriptors"?: Array<ActivityDescriptor>;
        "monacoLibPath"?: string;
        "onWorkflowUpdated"?: (event: CustomEvent<WorkflowDefinitionUpdatedArgs>) => void;
        "workflowDefinition"?: WorkflowDefinition;
    }
    interface ElsaWorkflowDefinitionEditorToolbox {
        "graph"?: Graph;
    }
    interface ElsaWorkflowDefinitionEditorToolboxActivities {
        "activityDescriptors"?: Array<ActivityDescriptor>;
        "graph"?: Graph;
    }
    interface ElsaWorkflowDefinitionPropertiesEditor {
        "onWorkflowPropsUpdated"?: (event: CustomEvent<WorkflowDefinitionPropsUpdatedArgs>) => void;
        "workflowDefinition"?: WorkflowDefinition;
    }
    interface ElsaWorkflowInstanceBrowser {
        "onWorkflowInstanceSelected"?: (event: CustomEvent<WorkflowInstanceSummary>) => void;
    }
    interface ElsaWorkflowInstanceProperties {
        "workflowDefinition"?: WorkflowDefinition;
    }
    interface ElsaWorkflowInstanceViewer {
        "activityDescriptors"?: Array<ActivityDescriptor>;
        "monacoLibPath"?: string;
        "workflowDefinition"?: WorkflowDefinition;
        "workflowInstance"?: WorkflowInstance;
    }
    interface ElsaWorkflowManager {
        "activityDescriptors"?: Array<ActivityDescriptor>;
        "monacoLibPath"?: string;
        "workflowDefinition"?: WorkflowDefinition;
        "workflowInstance"?: WorkflowInstance;
    }
    interface ElsaWorkflowPublishButton {
        "onExportClicked"?: (event: CustomEvent<any>) => void;
        "onImportClicked"?: (event: CustomEvent<File>) => void;
        "onNewClicked"?: (event: CustomEvent<any>) => void;
        "onPublishClicked"?: (event: CustomEvent<PublishClickedArgs>) => void;
        "onUnPublishClicked"?: (event: CustomEvent<any>) => void;
        "publishing"?: boolean;
    }
    interface ElsaWorkflowToolbar {
    }
    interface ElsaWorkflowToolbarMenu {
    }
    interface IntrinsicElements {
        "elsa-activity-properties-editor": ElsaActivityPropertiesEditor;
        "elsa-button-group": ElsaButtonGroup;
        "elsa-canvas": ElsaCanvas;
        "elsa-check-list-input": ElsaCheckListInput;
        "elsa-code-editor-input": ElsaCodeEditorInput;
        "elsa-context-menu": ElsaContextMenu;
        "elsa-dropdown-button": ElsaDropdownButton;
        "elsa-dropdown-input": ElsaDropdownInput;
        "elsa-flowchart": ElsaFlowchart;
        "elsa-form-panel": ElsaFormPanel;
        "elsa-input-control-switch": ElsaInputControlSwitch;
        "elsa-input-tags": ElsaInputTags;
        "elsa-input-tags-dropdown": ElsaInputTagsDropdown;
        "elsa-label-creator": ElsaLabelCreator;
        "elsa-label-editor": ElsaLabelEditor;
        "elsa-label-picker": ElsaLabelPicker;
        "elsa-labels-manager": ElsaLabelsManager;
        "elsa-labels-widget": ElsaLabelsWidget;
        "elsa-modal-dialog": ElsaModalDialog;
        "elsa-monaco-editor": ElsaMonacoEditor;
        "elsa-multi-line-input": ElsaMultiLineInput;
        "elsa-multi-text-input": ElsaMultiTextInput;
        "elsa-pager": ElsaPager;
        "elsa-panel": ElsaPanel;
        "elsa-radio-list-input": ElsaRadioListInput;
        "elsa-single-line-input": ElsaSingleLineInput;
        "elsa-slide-over-panel": ElsaSlideOverPanel;
        "elsa-studio": ElsaStudio;
        "elsa-switch-editor": ElsaSwitchEditor;
        "elsa-tooltip": ElsaTooltip;
        "elsa-workflow-definition-browser": ElsaWorkflowDefinitionBrowser;
        "elsa-workflow-definition-editor": ElsaWorkflowDefinitionEditor;
        "elsa-workflow-definition-editor-toolbox": ElsaWorkflowDefinitionEditorToolbox;
        "elsa-workflow-definition-editor-toolbox-activities": ElsaWorkflowDefinitionEditorToolboxActivities;
        "elsa-workflow-definition-properties-editor": ElsaWorkflowDefinitionPropertiesEditor;
        "elsa-workflow-instance-browser": ElsaWorkflowInstanceBrowser;
        "elsa-workflow-instance-properties": ElsaWorkflowInstanceProperties;
        "elsa-workflow-instance-viewer": ElsaWorkflowInstanceViewer;
        "elsa-workflow-manager": ElsaWorkflowManager;
        "elsa-workflow-publish-button": ElsaWorkflowPublishButton;
        "elsa-workflow-toolbar": ElsaWorkflowToolbar;
        "elsa-workflow-toolbar-menu": ElsaWorkflowToolbarMenu;
    }
}
export { LocalJSX as JSX };
declare module "@stencil/core" {
    export namespace JSX {
        interface IntrinsicElements {
            "elsa-activity-properties-editor": LocalJSX.ElsaActivityPropertiesEditor & JSXBase.HTMLAttributes<HTMLElsaActivityPropertiesEditorElement>;
            "elsa-button-group": LocalJSX.ElsaButtonGroup & JSXBase.HTMLAttributes<HTMLElsaButtonGroupElement>;
            "elsa-canvas": LocalJSX.ElsaCanvas & JSXBase.HTMLAttributes<HTMLElsaCanvasElement>;
            "elsa-check-list-input": LocalJSX.ElsaCheckListInput & JSXBase.HTMLAttributes<HTMLElsaCheckListInputElement>;
            "elsa-code-editor-input": LocalJSX.ElsaCodeEditorInput & JSXBase.HTMLAttributes<HTMLElsaCodeEditorInputElement>;
            "elsa-context-menu": LocalJSX.ElsaContextMenu & JSXBase.HTMLAttributes<HTMLElsaContextMenuElement>;
            "elsa-dropdown-button": LocalJSX.ElsaDropdownButton & JSXBase.HTMLAttributes<HTMLElsaDropdownButtonElement>;
            "elsa-dropdown-input": LocalJSX.ElsaDropdownInput & JSXBase.HTMLAttributes<HTMLElsaDropdownInputElement>;
            "elsa-flowchart": LocalJSX.ElsaFlowchart & JSXBase.HTMLAttributes<HTMLElsaFlowchartElement>;
            "elsa-form-panel": LocalJSX.ElsaFormPanel & JSXBase.HTMLAttributes<HTMLElsaFormPanelElement>;
            "elsa-input-control-switch": LocalJSX.ElsaInputControlSwitch & JSXBase.HTMLAttributes<HTMLElsaInputControlSwitchElement>;
            "elsa-input-tags": LocalJSX.ElsaInputTags & JSXBase.HTMLAttributes<HTMLElsaInputTagsElement>;
            "elsa-input-tags-dropdown": LocalJSX.ElsaInputTagsDropdown & JSXBase.HTMLAttributes<HTMLElsaInputTagsDropdownElement>;
            "elsa-label-creator": LocalJSX.ElsaLabelCreator & JSXBase.HTMLAttributes<HTMLElsaLabelCreatorElement>;
            "elsa-label-editor": LocalJSX.ElsaLabelEditor & JSXBase.HTMLAttributes<HTMLElsaLabelEditorElement>;
            "elsa-label-picker": LocalJSX.ElsaLabelPicker & JSXBase.HTMLAttributes<HTMLElsaLabelPickerElement>;
            "elsa-labels-manager": LocalJSX.ElsaLabelsManager & JSXBase.HTMLAttributes<HTMLElsaLabelsManagerElement>;
            "elsa-labels-widget": LocalJSX.ElsaLabelsWidget & JSXBase.HTMLAttributes<HTMLElsaLabelsWidgetElement>;
            "elsa-modal-dialog": LocalJSX.ElsaModalDialog & JSXBase.HTMLAttributes<HTMLElsaModalDialogElement>;
            "elsa-monaco-editor": LocalJSX.ElsaMonacoEditor & JSXBase.HTMLAttributes<HTMLElsaMonacoEditorElement>;
            "elsa-multi-line-input": LocalJSX.ElsaMultiLineInput & JSXBase.HTMLAttributes<HTMLElsaMultiLineInputElement>;
            "elsa-multi-text-input": LocalJSX.ElsaMultiTextInput & JSXBase.HTMLAttributes<HTMLElsaMultiTextInputElement>;
            "elsa-pager": LocalJSX.ElsaPager & JSXBase.HTMLAttributes<HTMLElsaPagerElement>;
            "elsa-panel": LocalJSX.ElsaPanel & JSXBase.HTMLAttributes<HTMLElsaPanelElement>;
            "elsa-radio-list-input": LocalJSX.ElsaRadioListInput & JSXBase.HTMLAttributes<HTMLElsaRadioListInputElement>;
            "elsa-single-line-input": LocalJSX.ElsaSingleLineInput & JSXBase.HTMLAttributes<HTMLElsaSingleLineInputElement>;
            "elsa-slide-over-panel": LocalJSX.ElsaSlideOverPanel & JSXBase.HTMLAttributes<HTMLElsaSlideOverPanelElement>;
            "elsa-studio": LocalJSX.ElsaStudio & JSXBase.HTMLAttributes<HTMLElsaStudioElement>;
            "elsa-switch-editor": LocalJSX.ElsaSwitchEditor & JSXBase.HTMLAttributes<HTMLElsaSwitchEditorElement>;
            "elsa-tooltip": LocalJSX.ElsaTooltip & JSXBase.HTMLAttributes<HTMLElsaTooltipElement>;
            "elsa-workflow-definition-browser": LocalJSX.ElsaWorkflowDefinitionBrowser & JSXBase.HTMLAttributes<HTMLElsaWorkflowDefinitionBrowserElement>;
            "elsa-workflow-definition-editor": LocalJSX.ElsaWorkflowDefinitionEditor & JSXBase.HTMLAttributes<HTMLElsaWorkflowDefinitionEditorElement>;
            "elsa-workflow-definition-editor-toolbox": LocalJSX.ElsaWorkflowDefinitionEditorToolbox & JSXBase.HTMLAttributes<HTMLElsaWorkflowDefinitionEditorToolboxElement>;
            "elsa-workflow-definition-editor-toolbox-activities": LocalJSX.ElsaWorkflowDefinitionEditorToolboxActivities & JSXBase.HTMLAttributes<HTMLElsaWorkflowDefinitionEditorToolboxActivitiesElement>;
            "elsa-workflow-definition-properties-editor": LocalJSX.ElsaWorkflowDefinitionPropertiesEditor & JSXBase.HTMLAttributes<HTMLElsaWorkflowDefinitionPropertiesEditorElement>;
            "elsa-workflow-instance-browser": LocalJSX.ElsaWorkflowInstanceBrowser & JSXBase.HTMLAttributes<HTMLElsaWorkflowInstanceBrowserElement>;
            "elsa-workflow-instance-properties": LocalJSX.ElsaWorkflowInstanceProperties & JSXBase.HTMLAttributes<HTMLElsaWorkflowInstancePropertiesElement>;
            "elsa-workflow-instance-viewer": LocalJSX.ElsaWorkflowInstanceViewer & JSXBase.HTMLAttributes<HTMLElsaWorkflowInstanceViewerElement>;
            "elsa-workflow-manager": LocalJSX.ElsaWorkflowManager & JSXBase.HTMLAttributes<HTMLElsaWorkflowManagerElement>;
            "elsa-workflow-publish-button": LocalJSX.ElsaWorkflowPublishButton & JSXBase.HTMLAttributes<HTMLElsaWorkflowPublishButtonElement>;
            "elsa-workflow-toolbar": LocalJSX.ElsaWorkflowToolbar & JSXBase.HTMLAttributes<HTMLElsaWorkflowToolbarElement>;
            "elsa-workflow-toolbar-menu": LocalJSX.ElsaWorkflowToolbarMenu & JSXBase.HTMLAttributes<HTMLElsaWorkflowToolbarMenuElement>;
        }
    }
}

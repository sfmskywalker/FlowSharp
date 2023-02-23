import {Activity, ActivityDescriptor, InputDescriptor, PropertyDescriptor, TabDefinition} from "../../../models";
import {WorkflowDefinition} from "./entities";

export const WorkflowEditorEventTypes = {
  WorkflowDefinition: {
    Imported: 'workflow-editor:workflow-definition:imported'
  },
  Activity: {
    PropertyChanged: 'workflow-editor:activity:property-changed'
  },
  WorkflowEditor: {
    Ready: 'workflow-editor:ready',
    WorkflowLoaded: 'workflow-editor:workflow-loaded'
  }
}

export const WorkflowPropertiesEditorEventTypes = {
  Displaying: 'workflow-properties:displaying'
}

export const ActivityPropertyPanelEvents = {
  Displaying: 'activity-property-panel:displaying'
}

export interface ActivityPropertyChangedEventArgs {
  activity: Activity;
  activityDescriptor: ActivityDescriptor;
  propertyName?: string;
  inputDescriptor?: InputDescriptor;
  workflowEditor?: HTMLElsaWorkflowDefinitionEditorElement;
}

export interface WorkflowDefinitionUpdatedArgs {
  workflowDefinition: WorkflowDefinition;
}

export interface WorkflowDefinitionPropsUpdatedArgs {
  workflowDefinition: WorkflowDefinition;
}

export interface WorkflowPropertiesEditorDisplayingArgs {
  model: WorkflowPropertiesEditorModel;
}

export interface TabModel {
  name: string;
  tab: TabDefinition;
}

export interface Widget {
  name: string;
  content: () => any;
  order?: number;
}

export interface PropertiesTabModel extends TabModel {
  Widgets: Array<Widget>;
}

export interface WorkflowPropertiesEditorModel {
  tabModels: Array<TabModel>;
}

export interface WorkflowDefinitionImportedArgs {
  workflowDefinition: WorkflowDefinition;
}

export interface WorkflowEditorReadyArgs {
  workflowEditor: HTMLElsaWorkflowDefinitionEditorElement;
}

export interface ActivityUpdatedArgs {
  originalId?: string;
  newId?: string;
  activity: Activity;
  activityDescriptor: ActivityDescriptor;
  // propertyName?: string;
  // propertyDescriptor?: PropertyDescriptor;
}

export interface ActivityIdUpdatedArgs {
  activity: Activity;
  activityDescriptor: ActivityDescriptor;
  originalId: string;
  newId: string;
}

export interface DeleteActivityRequestedArgs {
  activity: Activity;
}

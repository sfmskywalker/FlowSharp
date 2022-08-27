import {Component, h, Prop, getAssetPath} from '@stencil/core';
import {loadTranslations} from "../../../i18n/i18n-loader";
import {resources} from "./localizations";
import {i18n, t} from "i18next";
import {GetIntlMessage} from "../../../i18n/intl-message";
import Tunnel from "../../../../data/dashboard";
import {EventTypes, ConfigureDashboardMenuContext} from '../../../../models';
import {eventBus} from '../../../../services';
import { DropdownButtonItem, DropdownButtonOrigin } from '../../../controls/elsa-dropdown-button/models';

@Component({
  tag: 'elsa-studio-dashboard',
  shadow: false,
  assetsDirs: ['assets']
})
export class ElsaStudioDashboard {

  @Prop({attribute: 'culture', reflect: true}) culture: string;
  @Prop({attribute: 'base-path', reflect: true}) basePath: string = '';
  private i18next: i18n;
  private dashboardMenu: ConfigureDashboardMenuContext  = {
    data: {
      menuItems: [
        ['workflow-definitions', 'Workflow Definitions'],
        ['workflow-instances', 'Workflow Instances'],
        ['workflow-registry', 'Workflow Registry'],
      ],
      routes: [
        ['', 'elsa-studio-home', true],
        ['workflow-registry', 'elsa-studio-workflow-registry', true],
        ['workflow-registry/:id', 'elsa-studio-workflow-blueprint-view'],
        ['workflow-definitions', 'elsa-studio-workflow-definitions-list', true],
        ['workflow-definitions/:id', 'elsa-studio-workflow-definitions-edit'],
        ['workflow-instances', 'elsa-studio-workflow-instances-list', true],
        ['workflow-instances/:id', 'elsa-studio-workflow-instances-view'],
      ]
    }
  };

  async componentWillLoad() {
    this.i18next = await loadTranslations(this.culture, resources);
    await eventBus.emit(EventTypes.Dashboard.Appearing, this, this.dashboardMenu);
  }

  render() {

    const logoPath = getAssetPath('./assets/logo.png');
    const basePath = this.basePath || '';
    const IntlMessage = GetIntlMessage(this.i18next);

    const menuItemsNamespace = "menuItems"

    let menuItems = (this.dashboardMenu.data != null ? this.dashboardMenu.data.menuItems : [])
      .map(([route, label]) =>
        this.i18next.exists(`${menuItemsNamespace}:${route}`) ?
          [route, this.i18next.t(`${menuItemsNamespace}:${route}`)] :
          [route, label]
      );
    
    let routes = this.dashboardMenu.data != null ? this.dashboardMenu.data.routes : [];

    const renderFeatureMenuItem = (item: any, basePath: string) => {
      return (<stencil-route-link url={`${basePath}/${item[0]}`} anchorClass="elsa-text-gray-300 hover:elsa-bg-gray-700 hover:elsa-text-white elsa-px-3 elsa-py-2 elsa-rounded-md elsa-text-sm elsa-font-medium" activeClass="elsa-text-white elsa-bg-gray-900">
                <IntlMessage label={`${item[1]}`}/>
              </stencil-route-link>)
    }

    const renderFeatureRoute = (item: any, basePath: string) => {
      return (<stencil-route url={`${basePath}/${item[0]}`} component={`${item[1]}`} exact={item[2]}/>)
    }

    return (
      <div class="elsa-h-screen elsa-bg-gray-100">
        <nav class="elsa-bg-gray-800">
          <div class="elsa-px-4 sm:elsa-px-6 lg:elsa-px-8">
            <div class="elsa-flex elsa-items-center elsa-justify-between elsa-h-16">
              <div class="elsa-flex elsa-items-center">
                <div class="elsa-flex-shrink-0">
                  <stencil-route-link url={`${basePath}/`}>
                    <img class="elsa-h-8 elsa-w-8" src={logoPath}
                          alt="Workflow"/></stencil-route-link>
                </div>
                <div class="hidden md:elsa-block">
                  <div class="elsa-ml-10 elsa-flex elsa-items-baseline elsa-space-x-4">
                    {menuItems.map(item => renderFeatureMenuItem(item, basePath))}
                  </div>
                </div>
              </div>
              <elsa-dropdown-button text={'ibrahim'} items={ddlitems} btnClass='elsa-bg-gray-800 elsa-text-gray-300 elsa-w-full   elsa-shadow-sm elsa-px-4 elsa-py-2 elsa-inline-flex elsa-justify-center elsa-text-sm elsa-font-medium'
                                 origin={DropdownButtonOrigin.TopRight}
                                 onItemSelected={e => alert('gg')}/>
            </div>
          </div>

        </nav>
        <main>
          <stencil-router>
            <stencil-route-switch scrollTopOffset={0}>
              {routes.map(item => renderFeatureRoute(item, basePath))}
            </stencil-route-switch>
          </stencil-router>
        </main>

      </div>
    );
  }
}
const renderIcon = function () {
  return <svg class="elsa-mr-3 elsa-h-5 elsa-w-5 elsa-text-gray-400" fill="none" viewBox="0 0 24 24"
              stroke="currentColor">
    <circle cx="12" cy="12" r="10"/>
    <polygon points="10 8 16 12 10 16 10 8"/>
  </svg>
};

const ddlitems: Array<DropdownButtonItem> = [{'text':"log out" , value : "account/logout" }].map(x => {
  const text = x ?? t('Filters.Status.All');
  const item: DropdownButtonItem = {text: x.text, isSelected: false, value: x.value};

  return item
});

Tunnel.injectProps(ElsaStudioDashboard, ['culture', 'basePath']);

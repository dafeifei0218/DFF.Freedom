import { FreedomTemplatePage } from './app.po';

describe('abp-project-name-template App', function() {
  let page: FreedomTemplatePage;

  beforeEach(() => {
    page = new FreedomTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

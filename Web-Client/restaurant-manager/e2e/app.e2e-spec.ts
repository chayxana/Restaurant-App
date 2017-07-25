import { RestaurantManagerPage } from './app.po';

describe('restaurant-manager App', () => {
  let page: RestaurantManagerPage;

  beforeEach(() => {
    page = new RestaurantManagerPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});

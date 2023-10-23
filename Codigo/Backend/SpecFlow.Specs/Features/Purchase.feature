Feature: Purchase Products in the Shopping Cart

  As a customer on the platform,
  I want to be able to purchase the products that are already in my shopping cart,
  So that I can easily complete my online shopping.

  Scenario: Successful Checkout
    Given I am a customer on the platform
    And I have one or more products in my shopping cart
    When I proceed to checkout and confirm the purchase
    Then the system should create an order for the selected products
    And the system should provide an order confirmation

  Scenario: Empty Cart During Checkout
    Given I am a customer on the platform
    And I have no products in my shopping cart
    When I proceed to checkout and confirm the purchase
    Then the system should display an error message indicating that the cart is empty
    And I should not be able to complete the purchase
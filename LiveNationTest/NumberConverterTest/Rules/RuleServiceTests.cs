using FluentAssertions;
using NumberConverter.Rules;
using NumberConverter.Rules.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverterTest.Rules
{
    public class RuleServiceTests
    {
        [Fact]
        public void RuleService_IncorrectOperator_ErrorMessage()
        {
            Mock<IRuleStore> ruleStore = new Mock<IRuleStore>();

            RuleService ruleService = new RuleService(ruleStore.Object);

            RuleValidationResult result = ruleService.AddRule(new("divisible", 3, 0, "test"));

            result.Success.Should().Be(false);
            result.Message.Should().Be("Invalid Operator: valid operators are: DivisibleBy, Multiplication, Addition, Subtraction");
            result.Rule.Should().BeNull();
            ruleStore.Verify(rs => rs.StoreRule(It.IsAny<Rule>()), Times.Never);
        }

        [Fact]
        public void RuleService_DivideAndMultiplyZero_ErrorMessage()
        {
            Mock<IRuleStore> ruleStore = new Mock<IRuleStore>();

            RuleService ruleService = new RuleService(ruleStore.Object);

            RuleValidationResult result = ruleService.AddRule(new("divisibleby", 0, 0, "test"));
            RuleValidationResult result2 = ruleService.AddRule(new("multiplication", 0, 0, "test"));

            result.Success.Should().Be(false);
            result.Message.Should().Be("Invalid Operand: cannot divide by 0");
            result.Rule.Should().BeNull();

            result2.Success.Should().Be(false);
            result2.Message.Should().Be("Invalid Operand: cannot multiply by 0");
            result.Rule.Should().BeNull();
            ruleStore.Verify(rs => rs.StoreRule(It.IsAny<Rule>()), Times.Never);
        }

        [Fact]
        public void RuleService_EmptyReplacementString_ErrorMessage()
        {
            Mock<IRuleStore> ruleStore = new Mock<IRuleStore>();
            RuleService ruleService = new RuleService(ruleStore.Object);

            RuleValidationResult result = ruleService.AddRule(new("divisibleby", 3, 0, ""));

            result.Success.Should().Be(false);
            result.Message.Should().Be("Invalid ReplacementString: ReplacementString cannot be empty");
            result.Rule.Should().BeNull();
            ruleStore.Verify(rs => rs.StoreRule(It.IsAny<Rule>()), Times.Never);
        }

        [Fact]
        public void RuleService_ValidRules_SuccessResult()
        {
            Mock<IRuleStore> ruleStore = new Mock<IRuleStore>();
            RuleService ruleService = new RuleService(ruleStore.Object);

            RuleValidationResult result = ruleService.AddRule(new ("DivisibleBy", 3, 0, "test" ));
            RuleValidationResult result2 = ruleService.AddRule(new ("Multiplication", 3, 0, "test"));
            RuleValidationResult result3 = ruleService.AddRule(new ("Addition", 3, 0, "test"));
            RuleValidationResult result4 = ruleService.AddRule(new ("Subtraction", 3, 0, "test"));

            result.Success.Should().Be(true);
            result.Message.Should().Be("");
            result.Rule.Should().NotBeNull();

            result2.Success.Should().Be(true);
            result2.Message.Should().Be("");
            result.Rule.Should().NotBeNull();

            result3.Success.Should().Be(true);
            result3.Message.Should().Be("");
            result.Rule.Should().NotBeNull();

            result4.Success.Should().Be(true);
            result4.Message.Should().Be("");
            result.Rule.Should().NotBeNull();

            ruleStore.Verify(rs => rs.StoreRule(It.IsAny<Rule>()), Times.Exactly(4));
        }
    }
}

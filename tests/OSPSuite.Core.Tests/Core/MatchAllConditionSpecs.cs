﻿using OSPSuite.BDDHelper;
using OSPSuite.BDDHelper.Extensions;
using OSPSuite.Core.Domain.Descriptors;

namespace OSPSuite.Core
{
   public abstract class concern_for_MatchAllCondition : ContextSpecification<MatchAllCondition>
   {
      protected Tags _tags;
      protected IDescriptorCondition _match;
      protected IDescriptorCondition _doNotMatch;

      protected override void Context()
      {
         _tags = new Tags();
         _tags.Add(new Tag { Value = "tag1" });
         _tags.Add(new Tag { Value = "tag2" });
         sut = new MatchAllCondition();
      }
   }

   
   public class When_checking_if_a_match_all_matches_a_given_tag_set : concern_for_MatchAllCondition
   {
      [Observation]
      public void check_that_string_representation_is_accurate()
      {
         sut.ToString().ShouldBeEqualTo("ALL");
      }

      [Observation]
      public void should_always_return_true_if_the_tags_contain_the_matching_element()
      {
         sut.IsSatisfiedBy(_tags).ShouldBeTrue();
      }
     
   }
}	